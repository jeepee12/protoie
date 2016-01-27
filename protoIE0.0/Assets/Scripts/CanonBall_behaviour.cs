using UnityEngine;
using System.Collections;

public class CanonBall_behaviour : MonoBehaviour {

    public Rigidbody canonBall;
    public float speed; // only for testing speed to hard code.

    private float atk;

    // Use this for initialization
    void Start()
    {
        canonBall.velocity = (transform.forward) * speed;
    }
        
     
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            // canonBall.gameObject.SetActive(false); -- the idea is to remove the canonBall after it hit something.
        }
    }
}
