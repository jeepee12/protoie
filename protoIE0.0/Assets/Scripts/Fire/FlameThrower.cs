using UnityEngine;
using System.Collections;

public class FlameThrower : MonoBehaviour
{
    public GameObject m_fireEffect;
    public float m_AttackRate;

    private float m_Cooldown;

    private void OnTriggerStay(Collider objectColliding)
    {
        if (objectColliding.CompareTag("Enemy"))
        {
            
            //Repeat each X amount of time.
            if (Time.time > m_Cooldown)
            {
                m_Cooldown = Time.time + m_AttackRate;
                //Take the enemy health and reduce it by X
            }
        }
    }
}
