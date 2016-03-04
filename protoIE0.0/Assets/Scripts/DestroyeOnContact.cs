// To destroye any Object in the world by collision
// Use with caution as it may results in unintented bugs.


using UnityEngine;
using System.Collections;

public class DestroyeOnContact : MonoBehaviour
{
    public GameObject Destructable;
    public GameObject FireExplosion;
    public GameObject Fire;
    public GameObject Splash;

    EnemyStats m_EnemyStatsScript;

    private int m_Damage;

    void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Enemy")
        {
            // add HP remover here instead of Destroyer.
            m_EnemyStatsScript = hit.gameObject.transform.parent.GetComponent<EnemyStats>();
            m_EnemyStatsScript.TakeDamage(m_Damage);
        }

        if (hit.tag == "Isle")
        {
            Instantiate(FireExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (hit.tag == "Water")
        {
            Instantiate(Splash, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (hit.tag == "Untagged")
            return;
    }

    public void Damage(int newDamage)
    {
        m_Damage = newDamage;
    }
}