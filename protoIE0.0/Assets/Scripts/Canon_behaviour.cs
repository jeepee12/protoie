using UnityEngine;
using System.Collections;

public class Canon_behaviour : MonoBehaviour {

    // public var.
    public Rigidbody canon;
    public Rigidbody canonBall;
    public Rigidbody FireBall;
    public Transform canonHole;
    public GameObject FireCanon;
    public float fireRate;
    
    // private var.
    private float rotX;
    private float rotY;
    private float cooldown;
    private string selecArms;
    private float selecArmspos;


	
	void Start ()
    {
        GetComponent<Rigidbody>();
        selecArms = "one";
	}


    void Update()
    {

        if (Input.GetButtonDown("weaponselect") /*&& selecArms != "one"*/)
        {
            if (selecArmspos == 0)
            {
                selecArms = "one";
                cooldown = Time.time + 0.5f;
                selecArmspos = 1;
            }
            else if (selecArmspos == 1)
            {
                selecArms = "two";
                cooldown = Time.time + 0.5f;
                selecArmspos = 0;

            }
        }


        if (Input.GetButton("Fire1") && Time.time > cooldown)
        {
            if (selecArms == "one")
            {
                cooldown = Time.time + fireRate;
                Instantiate(canonBall, canonHole.position, canonHole.rotation); //as GameObject;
            }

            else if (selecArms == "two")
            {
                cooldown = Time.time + fireRate +2;
                Instantiate(FireBall, canonHole.position, canonHole.rotation); //as GameObject;
            }

            Instantiate (FireCanon, canonHole.position, canonHole.rotation);
            
        }   
    }


    // for testing purposes.
	void FixedUpdate ()
    {
        rotX = Input.GetAxis("Vertical");
        rotY = Input.GetAxis("Horizontal");

        canon.transform.Rotate(new Vector3(rotX, 0, 0) * 1);
        canon.transform.RotateAround(new Vector3(0,1.5f,-21f),Vector3.up , rotY);

        //maximum angle that the cannon can turn with.
        
        //if (canon.transform.rotation.y == -180 || canon.transform.rotation.y == 180)
        //{
        //    canon.transform.RotateAround(new Vector3(0, 0, -20), Vector3.zero, rotY);
        //}
        

	}
}
