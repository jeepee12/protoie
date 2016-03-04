using UnityEngine;
using System.Collections;

public class Camera3rd : MonoBehaviour {

    public Vector3 CameraPreset1Position = new Vector3(2.0f, 28.0f, -110.0f);
    public Vector3 CameraPreset1Rotation = new Vector3(55.0f, 0.0f, 0.0f);

    public Transform Bateau;

    // Use this for initialization
    void Start ()
    {
        transform.rotation = Quaternion.Euler(CameraPreset1Rotation);

    }
	
	// Update is called once per frame
	void LateUpdate()
    {
        // Calcul de la position de la camera
        Vector3 pos = Bateau.transform.position;

        pos += Bateau.forward * CameraPreset1Position.z;
        pos += Bateau.up * CameraPreset1Position.y;
        pos += Bateau.right * CameraPreset1Position.x;

        transform.position = pos;

        transform.LookAt(Bateau);
    }
}
