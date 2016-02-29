using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class Seagull_Flock_Sound_trigger : MonoBehaviour {

    AudioSource Flocksound;

	// Use this for initialization
	void Start ()
    {
        Flocksound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void OnTriggerEnter(Collider others)
    {
        if (others.CompareTag("Player"))
            Flocksound.Play();
        else
            Flocksound.Pause();
    }

    void OnTriggerExit(Collider others)
    {
        if (others.CompareTag("Player"))
            Flocksound.Stop();
        else
            Flocksound.Play();
    }
}
