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

    public override void move()
    {
        float valueH = Input.GetAxis("RightJoystickH");
        float valueV = Input.GetAxis("RightJoystickV");
        Vector3 myVector;

        myVector = new Vector3(1, 0, 0) * valueH;
        myVector += new Vector3(0, 0, 1) * valueV;

        myVector.Normalize();

        myVector *= 20;

        if ((valueH > deadZone || valueH < -deadZone) || (valueV > deadZone || valueV < -deadZone))
        {
            Direction.transform.position = myVector;
            Direction.transform.position += transform.position;

            Vector3 newPos = Vector3.MoveTowards(transform.position, Direction.transform.position, MoveSpeed * Time.deltaTime);

            float Distance = Vector3.Distance(Bateau.transform.position, newPos);

            if (Distance < maxDistance && Distance > minDistance)
                transform.position = newPos;
        }
    }

    public override Vector3 ShootVector()
    {
        return transform.position;
    }


}