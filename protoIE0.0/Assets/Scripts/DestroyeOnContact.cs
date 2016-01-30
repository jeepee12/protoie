// To destroye any Object in the world by collision
// Use with caution as it may results in unintented bugs.


using UnityEngine;
using System.Collections;

public class DestroyeOnContact : MonoBehaviour
{
    public GameObject Destructable;
    public GameObject FireExplosion;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }

        Instantiate(FireExplosion, transform.position, transform.rotation);
        Destroy(gameObject);

       
    }
}