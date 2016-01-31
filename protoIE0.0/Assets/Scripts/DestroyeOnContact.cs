// To destroye any Object in the world by collision
// Use with caution as it may results in unintented bugs.


using UnityEngine;
using System.Collections;

public class DestroyeOnContact : MonoBehaviour
{
    public GameObject Destructable;
    public GameObject FireExplosion;
    public GameObject Fire;

    void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Enemy")
        {
            Destroy(hit.gameObject);
        }

        else if (hit.tag == "flammable")
        {
            if (Destructable.tag == "FireBall")
                Instantiate(Fire, transform.position, transform.rotation);

            else
            {
                Instantiate(FireExplosion, transform.position, transform.rotation);
                Destroy(hit.gameObject);
            }

        }

        else if (hit.tag == "canonBall")
        {
            return;
        }

        Instantiate(FireExplosion, transform.position, transform.rotation);
        Destroy(gameObject);

       
    }
}