using UnityEngine;

public class BoundingLimitLeft : MonoBehaviour {

    private float xPosition;
    public GameObject mapGenerator;
    private MapGenerator mapGScript;

    private void Start()
    {
        mapGScript = mapGenerator.GetComponent<MapGenerator>();
        xPosition = transform.position.x * -1 - transform.localScale.x*2;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Enemy"))
        {
            NavMeshAgent nma = other.gameObject.GetComponent<NavMeshAgent>();
            nma.Warp(new Vector3(xPosition, other.transform.position.y, other.transform.position.z));
        }
        else if (other.gameObject.tag.Equals("Player"))
        {
            mapGScript.Collision();
            other.transform.position = new Vector3(xPosition, other.transform.position.y, other.transform.position.z);
        }
    }
}
