using UnityEngine;

public class Fireproof : MonoBehaviour
{
	public bool m_Flamable;
	public bool m_Oiled;
	public float m_OiledDuration;
	private bool m_OnFire;
	private float m_OiledTimerEnd;

	//Hp would be in the stats script of the game object
	public int hpTest = 10;

	void Update ()
	{   
		if (hpTest <= 0)
		{
			//Kill the object
			Destroy(gameObject);
		}

		if(m_Oiled)
		{
			if (Time.time > m_OiledTimerEnd)
			{
				m_Oiled = false;
			}
		}
 
	}

	public bool isFlamable()
	{
		return m_Flamable;
	}

	public void setIsOnFire(bool newOnFire)
	{
		m_OnFire = newOnFire;

		if(m_OnFire)
		{
			InvokeRepeating("dealDamage", 0, 0.5f);
		}
	}

	public void dealDamage()
	{
		int damage = 1;
		if(m_Oiled)
		{
			damage *= 2;
		}
		hpTest -= damage;
	}

	public bool isOnFire()
	{
		return m_OnFire;
	}

	public bool isOiled()
	{
		return m_Oiled;
	}

	public void StartOilTimer()
	{
		m_OiledTimerEnd = Time.time + m_OiledDuration;
	}
}
