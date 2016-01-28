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
    private float rotX;
    private float rotY;
    private float cooldown;


	
	void Start ()
    {
        GetComponent<Rigidbody>();  	
	}


    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > cooldown)
        {
            cooldown = Time.time + fireRate;
            Instantiate(canonBall, canonHole.position, canonHole.rotation); //as GameObject;
            Instantiate (FireCanon, canonHole.position, canonHole.rotation);
            
        } 

    }


    // for testing purposes.
	void FixedUpdate ()
    {
        rotX = Input.GetAxis("Vertical");
        rotY = Input.GetAxis("Horizontal");

        canon.transform.Rotate(new Vector3(rotX, 0, 0) * 1);
        canon.transform.RotateAround(new Vector3 (0,0,-20),Vector3.up , rotY);
        

	}
}
