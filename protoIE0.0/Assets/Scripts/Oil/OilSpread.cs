using UnityEngine;

public class OilSpread : MonoBehaviour
{
	public float m_SpreadMax;

	private float m_currentSpread;
	private GameObject m_FireEffect = null;

	private ParticleSystem m_FireEffectParticleEffect;
    
    private Transform m_FireTransform;
    
    private SphereCollider m_FireRange;
    private bool m_increaseFire = false;
    private bool m_increaseFireMax = false;

    void FixedUpdate ()
	{
		if(m_currentSpread < m_SpreadMax)
		{
			transform.localScale += new Vector3(10F * Time.deltaTime, 10F * Time.deltaTime, 0);

			m_currentSpread = transform.localScale.x;
		}

		if(m_FireEffect)
		{
			if(m_FireEffectParticleEffect)
			{
				if (m_currentSpread >= m_SpreadMax)
				{
                    //Do Once
                    if(!m_increaseFireMax)
                    {
                        m_increaseFireMax = true;
                        m_FireTransform = m_FireEffectParticleEffect.GetComponent<Transform>();
                        m_FireTransform.localScale = new Vector3(10, 10, 10);
                        float newLightRange = m_FireTransform.parent.GetComponent<SphereCollider>().radius = 15;

                        //Put fire to max
                        m_FireEffectParticleEffect.maxParticles = 5000;
                        m_FireEffectParticleEffect.emission.SetBursts(new ParticleSystem.Burst[]
                        {new ParticleSystem.Burst(0f, 500)});

                        m_FireTransform.GetComponentInChildren<Light>().range = newLightRange * 2;
                    }
                }
				else if(!m_increaseFire)
				{
                    m_increaseFire = true;
                    //Increment fire power
                    m_FireEffectParticleEffect.emission.SetBursts(new ParticleSystem.Burst[]
                    {
                        new ParticleSystem.Burst(1.0f, 100),
                        new ParticleSystem.Burst(2.0f, 200),
                        new ParticleSystem.Burst(3.0f, 500)
                    });
                }

                if(!m_increaseFireMax)
                {
                    m_FireEffectParticleEffect.maxParticles += 20;

                    if (!m_FireTransform)
                    {
                        m_FireTransform = m_FireEffectParticleEffect.GetComponent<Transform>();
                    }
                    else
                    {
                        if (!m_FireRange)
                        {
                            m_FireRange = m_FireTransform.parent.GetComponent<SphereCollider>();
                        }

                        float newX = 0, newY = 0, newZ = 0;
                        if(m_FireTransform.localScale.x < 10)
                        {
                            newX = 1F * Time.deltaTime;
                        }

                        if (m_FireTransform.localScale.y < 10)
                        {
                            newY = 1F * Time.deltaTime;
                        }

                        if (m_FireTransform.localScale.z < 10)
                        {
                            newZ = 1F * Time.deltaTime;
                        }
                        float newLightRange = m_FireRange.radius += newX;
                        m_FireTransform.GetComponentInChildren<Light>().range = newLightRange * 2;
                        m_FireTransform.localScale += new Vector3(newX, newY, newZ);
                    }
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
	