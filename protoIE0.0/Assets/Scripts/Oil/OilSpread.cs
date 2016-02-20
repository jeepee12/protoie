using UnityEngine;

public class OilSpread : MonoBehaviour
{
	public float m_SpreadMax;

	private float m_currentSpread;
	private GameObject m_FireEffect = null;

	private ParticleSystem m_FireEffectParticleEffect;

	void FixedUpdate ()
	{
		if(m_currentSpread < m_SpreadMax)
		{
			transform.localScale += new Vector3(0.1F, 0.1F, 0);

			m_currentSpread = transform.localScale.x;
		}

		if(m_FireEffect)
		{
			if(m_FireEffectParticleEffect)
			{
				if (m_currentSpread >= m_SpreadMax)
				{
					//m_FireEffectParticleEffect.maxParticles = 250;
					/*float constantMin = m_FireEffectParticleEffect.emission.rate.constantMin;
					float constantMax = m_FireEffectParticleEffect.emission.rate.constantMax;
					constantMin = 40;
					constantMax = 75;
					float radius = m_FireEffectParticleEffect.shape.radius;
					radius = 2;*/
				}
				else
				{
					//Increase size of fire over time
				}
			}
			else
			{
				m_FireEffectParticleEffect = m_FireEffect.GetComponentInChildren<ParticleSystem>();
			}

			//Need to follow the object since a sprite has a parent is not working.
			m_FireEffect.transform.position = gameObject.transform.position;
		}
	}


	void OnTriggerEnter(Collider objectColliding)
	{
		Fireproof objectFireproofScript = objectColliding.gameObject.GetComponent<Fireproof>();
		if(objectFireproofScript)
		{
			if (!objectFireproofScript.isOiled())
			{
				objectFireproofScript.StartOilTimer();
				objectFireproofScript.m_Oiled = true;
			}
		}
	}

	void OnTriggerStay(Collider objectColliding)
	{
		Fireproof objectFireproofScript = objectColliding.gameObject.GetComponent<Fireproof>();
		if (objectFireproofScript)
		{
			objectFireproofScript.StartOilTimer();
		}
	}

	public void FireEffect(GameObject fireEffect)
	{
		m_FireEffect = fireEffect;
	}

	void OnDestroy()
	{
		Destroy(m_FireEffect);
	}
}
	