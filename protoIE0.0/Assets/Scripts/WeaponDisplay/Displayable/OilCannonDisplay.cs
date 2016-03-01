using UnityEngine;
using System.Collections;

public class OilCannonDisplay : WeaponDisplay
{
    public GameObject Direction;
    public GameObject Bateau;
    public float MoveSpeed;

    public float minDistance = 0.5f;
    public float maxDistance = 10f;

    private float deadZone = 0.5f;
    private Vector3 CamPosition;

    public override void move()
    {
        float valueH = Input.GetAxis("RightJoystickH");
        float valueV = Input.GetAxis("RightJoystickV");

        if ((valueH > deadZone || valueH < -deadZone) || (valueV > deadZone || valueV < -deadZone))
        {
            Vector3 myVector;

            myVector = new Vector3(1, 0, 0) * valueH;
            myVector += new Vector3(0, 0, 1) * valueV;

            myVector.Normalize();

            myVector *= 20;

            Direction.transform.position = myVector;
            Direction.transform.position += transform.position;

            Vector3 newPos = Vector3.MoveTowards(transform.position, Direction.transform.position, MoveSpeed * Time.deltaTime);

            newPos.y = 0.2f;
            transform.position = newPos;

            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        CalcDistance();
    }

    public void CalcDistance()
    {
        float Distance = Vector3.Distance(Bateau.transform.position, transform.position);

        if (Distance < maxDistance && Distance > minDistance)
            readyToFire();
        else
            onCooldown();
    }

    public override Vector3 ShootVector()
    {
        return transform.position;
    }

    public override void Hide()
    {
        foreach (Transform child in gameObject.transform)
        {
            child.GetComponent<Renderer>().enabled = false;
        }
    }

    public override void Show()
    {
        foreach (Transform child in gameObject.transform)
        {
            child.GetComponent<Renderer>().enabled = true;
        }
    }
}