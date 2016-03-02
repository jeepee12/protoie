using UnityEngine;
using System.Collections;


public class FireSpread : MonoBehaviour
{
	private GameObject m_FireEffect;
    public FireOrigin m_FireOrigin;

    void Start()
    {
        m_FireEffect = m_FireOrigin.GetFireEffect();
    }

    void OnTriggerEnter(Collider objectColliding)
	{
		Fireproof objectFireproofScript = objectColliding.gameObject.GetComponent<Fireproof>();
		if(objectFireproofScript)
		{
			if (objectFireproofScript.isFlamable() && !objectFireproofScript.isOnFire())
			{
				if (objectColliding.name.Contains("Oil"))
				{
					GameObject fireEffectClone = (GameObject)Instantiate(m_FireEffect, objectColliding.transform.position, gameObject.transform.rotation);
					//Give a reference of the fire to the oil
					objectColliding.gameObject.GetComponent<OilSpread>().FireEffect(fireEffectClone);
				}
				else
				{
					GameObject fireEffectClone = (GameObject)Instantiate(m_FireEffect, Vector3.zero, gameObject.transform.rotation);
					fireEffectClone.transform.parent = objectColliding.transform;
					fireEffectClone.transform.localPosition = new Vector3(0, 0.5f, 0);
				}

                objectFireproofScript.setIsOnFire(true);
            }
		}
	}
}
