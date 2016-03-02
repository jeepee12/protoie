using UnityEngine;
using System.Collections;

public class OilCannonDisplay : WeaponDisplay
{
    public bool PS4Controller = false;
    public GameObject Direction;
    public GameObject Bateau;
    public float MoveSpeed;

    public float minShootDistance = 0.5f;
    public float maxShootDistance = 10f;

    public float maxDistance = 100.0f;

    private float deadZone = 0.5f;
    private Vector3 CamPosition;

    public override void move()
    {
        if (transform.position.y < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = Bateau.transform.position;
        }

        float valueH;
        float valueV;
        if (PS4Controller)
        {
            valueH = Input.GetAxis("RightJoystickHPS4");
            valueV = Input.GetAxis("RightJoystickVPS4");
        }
        else
        {
            valueH = Input.GetAxis("RightJoystickH");
            valueV = Input.GetAxis("RightJoystickV");
        }


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

            float Distance = Vector3.Distance(Bateau.transform.position, newPos);

            newPos.y = 0.2f;

            if (Distance < maxDistance)
                transform.position = newPos;
            
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        CalcDistance();
    }

    public override void Init()
    {
        base.Init();
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = Bateau.transform.position;
    }

    public void CalcDistance()
    {
        float Distance = Vector3.Distance(Bateau.transform.position, transform.position);

        if (Distance < maxShootDistance && Distance > minShootDistance)
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