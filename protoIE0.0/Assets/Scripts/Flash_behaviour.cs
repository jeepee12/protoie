using UnityEngine;
using System.Collections;

public class Flash_behaviour : MonoBehaviour {

    public GameObject FireExplosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Destroy(FireExplosion, 1);
    }
}
