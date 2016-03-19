using UnityEngine;
using System.Collections;

public class Camera3rd : MonoBehaviour {

    public bool PS4Controller = false;

    public Vector3 CameraPreset1Position = new Vector3(0.0f, 28.0f, -110.0f);

    public Transform Bateau;

    private Vector3 startPos =  Vector3.zero;

    public GameObject CamDir;
    public GameObject CamDir2;
    public int SideViewAngleMax;
    public int SideCamSpeed;

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

        CamDir.transform.position = posRight;
        CamDir2.transform.position = posLeft;

        if (startPos != Vector3.zero)
            posBehind.y = startPos.y;

        float valueH;

        Vector3 posUltime;
        if (PS4Controller)
            valueH = Input.GetAxis("RightJoystickHPS4");
        else
            valueH = Input.GetAxis("RightJoystickH");

        //Debug.Log("Value H : " + Mathf.Round(valueH));
        // Calcule de la position de la caméra
        if (valueH < -JoystickDeadZone && curCamSlide <= 0)
        {
            //Debug.Log("if1");
            curCamSlide -= Time.deltaTime * SideCamSpeed;
            if (curCamSlide < -1)
                curCamSlide = -1;
            posUltime = Vector3.Slerp(posBehind, posLeft, Mathf.Abs(curCamSlide));
        }
        else
        if (valueH > JoystickDeadZone && curCamSlide >= 0)
        {
            //Debug.Log("if2");
            curCamSlide += Time.deltaTime * SideCamSpeed;
            if (curCamSlide > 1)
                curCamSlide = 1;

            posUltime = Vector3.Slerp(posBehind, posRight, curCamSlide);
        }
        else
        if (curCamSlide < 0)
        {
            //Debug.Log("if3");
            curCamSlide += Time.deltaTime * SideCamSpeed;

            if (Mathf.Abs(curCamSlide) < 0.02)
                curCamSlide = 0;

            posUltime = Vector3.Slerp(posBehind, posLeft, Mathf.Abs(curCamSlide));
        }
        else
        {
            //Debug.Log("if4");
            curCamSlide -= Time.deltaTime * SideCamSpeed;

            if (Mathf.Abs(curCamSlide) < 0.02)
                curCamSlide = 0;

            posUltime = Vector3.Slerp(posBehind, posRight, curCamSlide);
        }

        //Debug.Log("Slide : " + curCamSlide);

        transform.position = posUltime;

        transform.LookAt(Bateau);
    }
}
