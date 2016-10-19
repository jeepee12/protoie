using UnityEngine;
using System.Collections;

public class FireCannonDisplay : WeaponDisplay {

    public bool PS4Controller = false;
    public GameObject VisualPlane;
    public GameObject Direction;
    public GameObject Bateau;

    public float RotateSpeed;

    public GameObject[] HideList;

    private float deadZone = 0.5f;

    public override void move()
    {
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
            Vector3 VectorDirection;

            VectorDirection = new Vector3(1, 0, 0) * valueH;
            VectorDirection += new Vector3(0, 0, 1) * valueV;

            VectorDirection.Normalize();

            VectorDirection *= 20;
            VectorDirection += transform.position;


            Direction.transform.position = VisualPlane.transform.position;
            Direction.transform.LookAt( VectorDirection);

            float step = RotateSpeed * Time.deltaTime;

            Quaternion Quat = Quaternion.RotateTowards(VisualPlane.transform.rotation, Direction.transform.rotation, step);

            Quaternion temp = VisualPlane.transform.rotation;

            VisualPlane.transform.rotation = Quat;

            Transform myNewT = VisualPlane.transform;

            myNewT.rotation = Quat;

            float AngleY = myNewT.localRotation.eulerAngles.y;

            if (AngleY <  90 || AngleY > 270)
                VisualPlane.transform.rotation = Quat;
            else
                VisualPlane.transform.rotation = temp;
        }
    }

    public override Vector3 ShootVector()
    {
        return VisualPlane.transform.forward;
    }

    public override void Hide()
    {
        foreach (GameObject toHide in HideList)
        {
            toHide.GetComponent<Renderer>().enabled = false;
        }
    }

    public override void Show()
    {
        foreach (GameObject toShow in HideList)
        {
            toShow.GetComponent<Renderer>().enabled = true;
        }

        base.Show();
    }

}
