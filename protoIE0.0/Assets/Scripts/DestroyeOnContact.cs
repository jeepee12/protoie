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
            // add HP remover here instead of Destroyer.
            Destroy(hit.gameObject);
        }

        else if (hit.tag == "canonBall" || hit.tag == "Player" || hit.tag == "Untagged")
        {
            return;
        }

        else if (hit.tag == "Water")
        {
            // need to add water splash effect.
            Destroy(gameObject);
            return;
        }

        Instantiate(FireExplosion, transform.position, transform.rotation);
        Destroy(gameObject);

       
    }
}