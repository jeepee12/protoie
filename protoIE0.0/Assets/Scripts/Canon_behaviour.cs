using UnityEngine;
using System.Collections;

public class Canon_behaviour : MonoBehaviour {

    // public var.
    public Rigidbody canon;
    public GameObject canonBall;
    public GameObject FireBall;
    public Transform canonHole;
    public GameObject FireCanon;
    public float fireRate;
    
    // private var.
    private float rotX;
    private float rotY;
    private float cooldown1;
    private float cooldown2;


	
	void Start ()
    {
        GetComponent<Rigidbody>();  	
	}


    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > cooldown1)
        {
            cooldown1 = Time.time + fireRate;
            Instantiate(canonBall, canonHole.position, canonHole.rotation); //as GameObject;
            Instantiate (FireCanon, canonHole.position, canonHole.rotation);
            
        }

        if (Input.GetButton("Fire2") && Time.time > cooldown2)
        {
            cooldown2 = Time.time + fireRate +5;
            Instantiate(FireBall , canonHole.position, canonHole.rotation); //as GameObject;
            Instantiate(FireCanon, canonHole.position, canonHole.rotation);

        }

    }


    // for testing purposes.
	void FixedUpdate ()
    {
        rotX = Input.GetAxis("Vertical");
        rotY = Input.GetAxis("Horizontal");

        canon.transform.Rotate(new Vector3(rotX, 0, 0) * 1);
        canon.transform.RotateAround(new Vector3(0,1.5f,-21f),Vector3.up , rotY);

        //if (canon.transform.rotation.y == -180 || canon.transform.rotation.y == 180)
        //{
        //    canon.transform.RotateAround(new Vector3(0, 0, -20), Vector3.zero, rotY);
        //}
        

	}
}
