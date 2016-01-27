using UnityEngine;
using System.Collections;

public class Canon_Rotation_behaviour : MonoBehaviour {

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
        if (Input.GetButton("Fire") && Time.time > cooldown)
        {
            cooldown = Time.time + fireRate;
            Instantiate (canonBall, canonHole.position, canonHole.rotation);
            Instantiate (FireCanon, canonHole.position, canonHole.rotation);
            
            
        } 

    }

	void FixedUpdate ()
    {
        rotX = Input.GetAxis("Vertical");
        rotY = Input.GetAxis("Horizontal");

        canon.transform.Rotate(new Vector3(rotX, 0, 0) * 1);
        canon.transform.Translate(new Vector3(rotY, 0, 0));
        

	}
}
