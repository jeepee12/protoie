using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class Seagull_Flock_Sound_trigger : MonoBehaviour {

    AudioSource Flocksound;

	// Use this for initialization
	void Start ()
    {
        Flocksound = GetComponent<AudioSource>();
        Flocksound.Stop();
    }
	
	// Update is called once per frame
	void OnTriggerEnter(Collider others)
    {
        if (others.CompareTag("Player"))
            Flocksound.Play();
        
    }

    void OnTriggerExit(Collider others)
    {
        if (others.CompareTag("Player"))
            Flocksound.Stop();
    }
}
