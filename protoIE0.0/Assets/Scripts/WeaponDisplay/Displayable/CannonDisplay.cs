using UnityEngine;
using System.Collections;

public class CannonDisplay : WeaponDisplay
{
    public float angleMax = 15;

    public override void move()
    {/*
        float valueH = Input.GetAxis("RightJoystickH");
        float valueV = Input.GetAxis("RightJoystickV");
        Vector3 myVector;

        myVector = new Vector3(1, 0, 0) * valueH;
        myVector += new Vector3(0, 0, 1) * valueV;

        myVector.Normalize();

        myVector *= 20;*/

    }

    public override Vector3 ShootVector()
    {
        return transform.forward;
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
