using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

    public Transform mainCam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = mainCam.rotation;
    }
}
