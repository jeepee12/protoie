using UnityEngine;
using System.Collections;

public class FlameThrower : MonoBehaviour
{
    public GameObject m_fireEffect;
    public float m_AttackRate;

    private float m_Cooldown;
    private int m_Damage;

    EnemyStats m_EnemyStatsScript;

    private void OnTriggerStay(Collider objectColliding)
    {
        if (objectColliding.CompareTag("Enemy"))
        {
            //Repeat each X amount of time.
            if (Time.time > m_Cooldown)
            {
                m_Cooldown = Time.time + m_AttackRate;
                //Take the enemy health and reduce it by X
                m_EnemyStatsScript = objectColliding.gameObject.transform.parent.GetComponent<EnemyStats>();
                m_EnemyStatsScript.TakeDamage(m_Damage);
            }
        }
    }

    public void Damage(int newDamage)
    {
        m_Damage = newDamage;
    }
}
