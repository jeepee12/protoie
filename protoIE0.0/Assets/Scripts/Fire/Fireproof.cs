using UnityEngine;

public class Fireproof : MonoBehaviour
{
	public bool m_Flamable = true;
	public bool m_Oiled = false;
	public float m_OiledDurationSeconds = 10;
    public float m_FireTicksSeconds = 0.5f;
    public float m_FireDurationSeconds = 15;

    public float m_FireDamage = 5;

    EnemyStats m_EnemyStatsScript;

    private float m_OiledTimerEnd;
    private float m_FireDuration;
    private bool m_OnFire;

    void Start()
    {
        m_EnemyStatsScript = gameObject.GetComponent<EnemyStats>();
    }

	void Update ()
	{   
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
		if(m_OnFire && m_EnemyStatsScript)
		{
            float ZeroDividerProtection = m_EnemyStatsScript.GetFireResistance();
            if (ZeroDividerProtection == 0)
            {
                ZeroDividerProtection = 1;
            }
            m_FireDuration = (m_FireDurationSeconds / ZeroDividerProtection) + Time.time;

            InvokeRepeating("dealDamage", 0, m_FireTicksSeconds);
		}
	}

	public void dealDamage()
	{
        if (Time.time >= m_FireDuration)
        {
            //Destroy the fire object
            GameObject fireEffect = transform.GetChild(0).transform.GetChild(0).gameObject;
            Destroy(fireEffect);
            m_OnFire = false;
            CancelInvoke();
            return;
        }

		if(m_Oiled)
		{
            m_FireDamage *= 2;
		}
        m_EnemyStatsScript.TakeDamage(m_FireDamage);
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
		m_OiledTimerEnd = Time.time + m_OiledDurationSeconds;
	}
}
