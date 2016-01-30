using UnityEngine;
using System.Collections;

public class canonFlash_behaviour : MonoBehaviour {

    public GameObject canonFlash;
	
	// Update is called once per frame
	void Update ()
    {
        Destroy(canonFlash, 2.5f);
    }
}
