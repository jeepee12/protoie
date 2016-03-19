using UnityEngine;
using System.Collections;

public class Camera3rd : MonoBehaviour {

    public bool PS4Controller = false;

    public Vector3 CameraPreset1Position = new Vector3(0.0f, 28.0f, -110.0f);

    public Transform Bateau;

    private Vector3 startPos =  Vector3.zero;
   
    public int SideViewAngleMax;
    public int SideCamSpeed;

    public bool ReverseSideLookJoystick = false;

    private float curCamSlide;
    private float JoystickDeadZone = 0.5f;

    // Use this for initialization
    void Start ()
    {
        LateUpdate();

        startPos = transform.position;
    }
	
	// Update is called once per frame
	void LateUpdate()
    {

        // Calcul des différente position de la caméra
        Vector3 posBehind = Bateau.transform.position;
        Vector3 posRight;
        Vector3 posLeft;

        posBehind += Bateau.forward * CameraPreset1Position.z;
        posBehind += Bateau.up * CameraPreset1Position.y;
        posBehind += Bateau.right * CameraPreset1Position.x;

        float pourc = SideViewAngleMax / 90.0f;

        posRight = -Bateau.transform.forward * pourc;
        posRight += Bateau.transform.right * (1- pourc);

        posRight.Normalize();


        posLeft = -Bateau.transform.forward * pourc;
        posLeft += -Bateau.transform.right * (1 - pourc);

        posLeft.Normalize();


        posRight *= Vector3.Distance(posBehind, Bateau.position);
        posLeft *= Vector3.Distance(posBehind, Bateau.position);

        posRight += Bateau.transform.position;
        posLeft += Bateau.transform.position;

        posRight.y = startPos.y;
        posLeft.y  = startPos.y;

        if (startPos != Vector3.zero)
            posBehind.y = startPos.y;

        float valueH;

        Vector3 posUltime;
        if (PS4Controller)
            valueH = Input.GetAxis("RightJoystickHPS4");
        else
            valueH = Input.GetAxis("RightJoystickH");

        // Calcule de la position de la caméra
        if (valueH < -JoystickDeadZone && curCamSlide <= 0)
        {
            curCamSlide -= Time.deltaTime * SideCamSpeed;
            if (curCamSlide < -1)
                curCamSlide = -1;

            if (ReverseSideLookJoystick)
                posUltime = Vector3.Slerp(posBehind, posLeft, Mathf.Abs(curCamSlide));
            else
                posUltime = Vector3.Slerp(posBehind, posRight, Mathf.Abs(curCamSlide));
        }
        else
        if (valueH > JoystickDeadZone && curCamSlide >= 0)
        {
            curCamSlide += Time.deltaTime * SideCamSpeed;
            if (curCamSlide > 1)
                curCamSlide = 1;

            if (ReverseSideLookJoystick)
                posUltime = Vector3.Slerp(posBehind, posRight, Mathf.Abs(curCamSlide));
            else
                posUltime = Vector3.Slerp(posBehind, posLeft, Mathf.Abs(curCamSlide));
        }
        else
        if (curCamSlide < 0)
        {
            curCamSlide += Time.deltaTime * SideCamSpeed;

            if (Mathf.Abs(curCamSlide) < 0.1)
                curCamSlide = 0;

            if (ReverseSideLookJoystick)
                posUltime = Vector3.Slerp(posBehind, posLeft, Mathf.Abs(curCamSlide));
            else
                posUltime = Vector3.Slerp(posBehind, posRight, Mathf.Abs(curCamSlide));
        }
        else
        if (curCamSlide > 0)
        {
            curCamSlide -= Time.deltaTime * SideCamSpeed;

            if (Mathf.Abs(curCamSlide) < 0.1)
                curCamSlide = 0;

            if (ReverseSideLookJoystick)
                posUltime = Vector3.Slerp(posBehind, posRight, Mathf.Abs(curCamSlide));
            else
                posUltime = Vector3.Slerp(posBehind, posLeft, Mathf.Abs(curCamSlide));
        }
        else
            posUltime = posBehind;

        transform.position = posUltime;

        transform.LookAt(Bateau);
    }
}
   
