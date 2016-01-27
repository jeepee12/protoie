using UnityEngine;

public class FireSpread : MonoBehaviour
{
	public GameObject m_fireEffect;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter(Collider objectColliding)
	{
		Fireproof objectFireproofScript = objectColliding.gameObject.GetComponent<Fireproof>();
		if (objectFireproofScript.isFlamable() && !objectFireproofScript.isOnFire())
		{
			GameObject fireEffectClone = (GameObject)Instantiate(m_fireEffect, Vector3.zero, objectColliding.transform.rotation);
			fireEffectClone.transform.parent = objectColliding.transform;
			fireEffectClone.transform.localPosition = new Vector3(0, objectColliding.transform.localScale.y/2, 0);
			objectFireproofScript.setIsOnFire(true);
		}
	}
}
