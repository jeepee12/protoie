using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{

    public GameObject player;

    public int CameraPreset = 1;

    public Vector3 CameraPreset1Position = new Vector3(0.0f, 100.0f, -150.0f);
    public Vector3 CameraPreset1Rotation = new Vector3(55.0f, 0.0f, 0.0f);

    public Vector3 CameraPreset2Position = new Vector3(0.0f, 150.0f, 0.0f);
    public Vector3 CameraPreset2Rotation = new Vector3(90.0f, 0.0f, 0.0f);

    private Vector3 offset;

    void Start()
    {

        switch (CameraPreset)
        {
            case 1:
                transform.position = CameraPreset1Position;
                transform.eulerAngles = CameraPreset1Rotation;
                break;

            case 2:
                transform.position = CameraPreset2Position;
                transform.eulerAngles = CameraPreset2Rotation;
                break;

            default:
                transform.position = CameraPreset1Position;
                transform.eulerAngles = CameraPreset1Rotation;
                break;

        }

        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

}