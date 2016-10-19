using UnityEngine;

public class OilBarrel : MonoBehaviour
{
    public GameObject m_ExplosionEffect;
    public GameObject m_OilEffect;

    private Transform m_ToDestroy;
    private Transform m_Target;
    private Vector3 m_DepartPos;
    private Rigidbody m_MyRigidBody;
    private bool doOnce = true;

    void Start()
    {
        m_DepartPos = transform.position;
        m_MyRigidBody = gameObject.GetComponent<Rigidbody>();
        GameObject[] objects = GameObject.FindGameObjectsWithTag("MapGenerator");
        m_ToDestroy = objects[0].GetComponent<MapGenerator>().ToDestroy;
    }

    private void OnTriggerEnter(Collider objectColliding)
    {
        Debug.Log(objectColliding.name);
        if(objectColliding.gameObject.tag.Contains("Enemy") || objectColliding.gameObject.tag.Contains("Water") || objectColliding.gameObject.tag.Contains("Isles"))
        {
            Vector3 OilSpawnPosition;

            OilSpawnPosition = gameObject.transform.position;
            OilSpawnPosition.y += 0.05f;

            Instantiate(m_ExplosionEffect, gameObject.transform.position, gameObject.transform.rotation);

            GameObject oilEffectClone = (GameObject) Instantiate(m_OilEffect, OilSpawnPosition, m_OilEffect.transform.rotation);
            oilEffectClone.transform.parent = m_ToDestroy;

            Destroy(gameObject);
        }
        if(objectColliding.name.Contains("Bounding"))
        {
            m_MyRigidBody.velocity = new Vector3(0,-9,0);
            return;
        }
    }

    public void GiveTarget(Transform target)
    {
        m_Target = target;
    }

    private void FixedUpdate()
    {
        if(m_Target && doOnce)
        {
            doOnce = false;
            m_MyRigidBody.velocity = BallisticVelocity(m_Target, 45);
        }
    }

    private Vector3 BallisticVelocity(Transform target, float angle)
    {
        Vector3 dir = target.position - m_DepartPos; // get Target Direction
        float height = dir.y; // get height difference
        dir.y = 0; // retain only the horizontal direction
        float dist = dir.magnitude; // get horizontal distance
        float a = angle * Mathf.Deg2Rad; // Convert angle to radians
        dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle.
        dist += height / Mathf.Tan(a); // Correction for small height differences

        // Calculate the velocity magnitude
        float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * dir.normalized; // Return the velocity vector.
    }
}
