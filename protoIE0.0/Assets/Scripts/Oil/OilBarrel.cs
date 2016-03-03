using UnityEngine;

public class OilBarrel : MonoBehaviour
{
    public GameObject m_ExplosionEffect;
    public GameObject m_OilEffect;

    public float m_Speed = 1f;

    private Transform m_Target;
    private Vector3 m_DepartPos;
    private Rigidbody m_MyRigidBody;
    private bool doOnce = true;

    void Start()
    {
        m_DepartPos = transform.position;
        m_MyRigidBody = gameObject.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider objectColliding)
    {
        if(objectColliding.gameObject.tag.Contains("Enemy") || objectColliding.gameObject.tag.Contains("Water") || objectColliding.gameObject.tag.Contains("Isles"))
        {
            Vector3 OilSpawnPosition;

            OilSpawnPosition = gameObject.transform.position;
            OilSpawnPosition.y += 0.05f;

            Instantiate(m_ExplosionEffect, gameObject.transform.position, gameObject.transform.rotation);
            Instantiate(m_OilEffect, OilSpawnPosition, m_OilEffect.transform.rotation);
            Destroy(gameObject);
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
