using UnityEngine;

public class OilBarrel : MonoBehaviour
{
    public GameObject m_ExplosionEffect;
    public GameObject m_OilEffect;

    private void OnCollisionEnter(Collision objectColliding)
    {
        Vector3 OilSpawnPosition;

        OilSpawnPosition = gameObject.transform.position;
        OilSpawnPosition.y = 0.001f;

        Instantiate(m_ExplosionEffect, gameObject.transform.position, gameObject.transform.rotation);
        Instantiate(m_OilEffect, OilSpawnPosition, m_OilEffect.transform.rotation);
        Destroy(gameObject);
    }
}
