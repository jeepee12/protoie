using UnityEngine;

public class WaterTrail : MonoBehaviour {

    public Navigation BoatNavigationScript;

    private ParticleSystem myParticleSystem;

	// Use this for initialization
	void Start ()
    {
        myParticleSystem = gameObject.GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        myParticleSystem.startLifetime = BoatNavigationScript.boatSpeed;
        //myParticleSystem.emission.rate = BoatNavigationScript.speed * 10;
    }
}
