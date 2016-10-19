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
        Fireproof objectFireproofScript;
        GameObject GOColliding;
        if (objectColliding.tag.Contains("Enemy"))
        {
            objectFireproofScript = objectColliding.gameObject.transform.parent.GetComponent<Fireproof>();
            GOColliding = objectColliding.gameObject.transform.parent.gameObject.transform.GetChild(0).gameObject;
        }
        else
        {
            objectFireproofScript = objectColliding.gameObject.GetComponent<Fireproof>();
            GOColliding = objectColliding.gameObject;
        }
		
		if(objectFireproofScript)
		{
			if (objectFireproofScript.isFlamable() && !objectFireproofScript.isOnFire())
			{
				if (GOColliding.name.Contains("Oil"))
				{
					GameObject fireEffectClone = (GameObject)Instantiate(m_FireEffect, GOColliding.transform.position, gameObject.transform.rotation);
                    //Give a reference of the fire to the oil
                    GOColliding.gameObject.GetComponent<OilSpread>().FireEffect(fireEffectClone);
				}
				else
				{
					GameObject fireEffectClone = (GameObject)Instantiate(m_FireEffect, Vector3.zero, gameObject.transform.rotation);
					fireEffectClone.transform.parent = GOColliding.transform;
					fireEffectClone.transform.localPosition = new Vector3(0, 0.5f, 0);
				}

                objectFireproofScript.setIsOnFire(true);
            }
		}
	}
}
