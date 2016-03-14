using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {

    public GameObject toFollow;

    private Vector3 startPos;

	// Use this for initialization
	void Start ()
    {
        startPos = toFollow.transform.position;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 pos = toFollow.transform.position;

        pos.y = startPos.y;

        transform.position = pos;

        transform.rotation = toFollow.transform.rotation;
	}
}
