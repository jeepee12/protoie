using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]

public class Sound_manager_fire : MonoBehaviour
{
    public float bpm = 140.0F;
    public int numBeatsPerSegment = 16;
    public AudioClip[] clips = new AudioClip[2];
    private AudioSource fireSound;
    private double nextEventTime;
    private bool running = false;    
    private int mainID;
    private int c1;

    void Start()
    {
        fireSound = GetComponent<AudioSource>();
        nextEventTime = AudioSettings.dspTime + 2.0F;      //Debug.Log(nextEventTime);
        fireSound.Play();
        running = true;
        mainID = clips[1].GetInstanceID();

        if (!fire.activeInHierarchy)
            c1 = 0;

        c1 = 1234;
    }

    void Update()
    {
        if (!running)
            return;

        double time = AudioSettings.dspTime;
        if (time + 1.0F > nextEventTime && c1 == 0)
        {
            //Debug.Log("Scheduled source to start at time " + nextEventTime);
            nextEventTime += 60.0F / bpm * numBeatsPerSegment;
            fireSound.clip = clips[1];
            fireSound.loop = true;
            fireSound.Play();
            c1 = 1;
        }
    }   
}
