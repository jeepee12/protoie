using UnityEngine;
using System.Collections;

public class Canon_behaviour : MonoBehaviour {

	// public var.
	public Rigidbody canon;
	public GameObject canonBall;
	public Transform canonHole;
	public GameObject FireCanon;
	public float fireRate;
	
	// private var.
	private float cooldown;
	


	
	void Start ()
	{
		canon = GetComponent<Rigidbody>();
		
	}


	void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > cooldown)
		{
            Instantiate(canonBall, canonHole.position, canonHole.rotation); //as GameObject;
			Instantiate (FireCanon, canonHole.position, canonHole.rotation);
            cooldown = Time.time + fireRate;		
		}   
	}
}
