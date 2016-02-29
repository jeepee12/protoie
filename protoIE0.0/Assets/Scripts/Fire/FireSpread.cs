using UnityEngine;
using System.Collections;


public class FireSpread : MonoBehaviour
{
	public GameObject m_fireEffect;

    void Start()
    {

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
					GameObject fireEffectClone = (GameObject)Instantiate(m_fireEffect, objectColliding.transform.position, gameObject.transform.rotation);
					//Give a reference of the fire to the oil
					objectColliding.gameObject.GetComponent<OilSpread>().FireEffect(fireEffectClone);
                    

				}
				else
				{
					GameObject fireEffectClone = (GameObject)Instantiate(m_fireEffect, Vector3.zero, gameObject.transform.rotation);
					fireEffectClone.transform.parent = objectColliding.transform;
					fireEffectClone.transform.localPosition = new Vector3(0, objectColliding.transform.localScale.y / 2, 0);
				}

                objectFireproofScript.setIsOnFire(true);

                
            }

		}
		
	}
}
