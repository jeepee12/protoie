using UnityEngine;

public class OilBarrel : MonoBehaviour
{
    public GameObject m_ExplosionEffect;
    public GameObject m_OilEffect;

    private void OnCollisionEnter(Collision objectColliding)
    {
<<<<<<< HEAD
        if(objectColliding.gameObject.tag.Contains("Enemy") || objectColliding.gameObject.tag.Contains("Water") || objectColliding.gameObject.tag.Contains("Wall"))
        {
            Vector3 OilSpawnPosition;
=======
        //Debug.Log(objectColliding.gameObject.name);
        Vector3 OilSpawnPosition;
>>>>>>> ff02b1b284b911ce2359d326f1ffaa47276d4c39

            OilSpawnPosition = gameObject.transform.position;
            OilSpawnPosition.y = 0.05f;

            Instantiate(m_ExplosionEffect, gameObject.transform.position, gameObject.transform.rotation);
            Instantiate(m_OilEffect, OilSpawnPosition, m_OilEffect.transform.rotation);
            Destroy(gameObject);
        }
    }
}
