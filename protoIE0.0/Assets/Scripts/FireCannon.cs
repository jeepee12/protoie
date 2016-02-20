using UnityEngine;
using System.Collections;

public class FireCannon : MonoBehaviour
{
	public Transform m_CannonHole;
	public GameObject m_FireAttack;
	public bool m_WeaponSelected;
	public float m_AttackCooldown;
	public float m_AttackDuration;

	private float m_Cooldown;
	
	void Start ()
	{
		m_Cooldown = 0;
	}
	
	void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > m_Cooldown)
		{
			m_Cooldown = Time.time + m_AttackCooldown;
			GameObject fireEffectClone = (GameObject)Instantiate(m_FireAttack, m_CannonHole.transform.position, m_CannonHole.transform.rotation);
			fireEffectClone.transform.parent = m_CannonHole.transform;
			Destroy(fireEffectClone, m_AttackDuration);
		}
	}
}
