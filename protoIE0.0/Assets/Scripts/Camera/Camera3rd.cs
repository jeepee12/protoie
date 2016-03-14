using UnityEngine;
using System.Collections;

public class Camera3rd : MonoBehaviour {

    public Vector3 CameraPreset1Position = new Vector3(0.0f, 28.0f, -110.0f);

    public Transform Bateau;

    private Vector3 startPos =  Vector3.zero;

    // Use this for initialization
    void Start ()
    {
        LateUpdate();

        startPos = transform.position;
    }
	
	// Update is called once per frame
	void LateUpdate()
    {
        // Calcul de la position de la camera
        Vector3 pos = Bateau.transform.position;

        pos += Bateau.forward * CameraPreset1Position.z;
        pos += Bateau.up * CameraPreset1Position.y;
        pos += Bateau.right * CameraPreset1Position.x;

        if (startPos != Vector3.zero)
            pos.y = startPos.y;

        transform.position = pos;

        transform.LookAt(Bateau);
    }
}
