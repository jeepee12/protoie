using UnityEngine;
using System.Collections;

public class WindForce : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider WindSource)
    {
        if (WindSource.tag == "Wind")
        {
            //me.applyforce
            this.GetComponent<Rigidbody>().AddForce(1.0f,0.0f,0.0f,ForceMode.Impulse);
        }
    }
}
