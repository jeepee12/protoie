using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float m_Time;

	void Start ()
    {
        Destroy(gameObject, m_Time);	
	}
}
