using UnityEngine;

public class OilBarrel : MonoBehaviour
{
    public GameObject m_ExplosionEffect;
    public GameObject m_OilEffect;

    private void OnCollisionEnter(Collision objectColliding)
    {
        if(objectColliding.gameObject.tag.Contains("Enemy") || objectColliding.gameObject.tag.Contains("Water") || objectColliding.gameObject.tag.Contains("Wall"))
        {
            Vector3 OilSpawnPosition;

            OilSpawnPosition = gameObject.transform.position;
            OilSpawnPosition.y = 0.05f;

            Instantiate(m_ExplosionEffect, gameObject.transform.position, gameObject.transform.rotation);
            Instantiate(m_OilEffect, OilSpawnPosition, m_OilEffect.transform.rotation);
            Destroy(gameObject);
        }
    }
}
