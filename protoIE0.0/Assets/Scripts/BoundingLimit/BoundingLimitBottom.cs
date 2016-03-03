using UnityEngine;

public class BoundingLimitBottom: MonoBehaviour {

    private float zPosition;
    public GameObject mapGenerator;
    private MapGenerator mapGScript;

    private void Start()
    {
        mapGScript = mapGenerator.GetComponent<MapGenerator>();
        zPosition = transform.position.z * -1 - transform.localScale.z*2;
    }

    void OnTriggerEnter(Collider other)
    {
        mapGScript.Collision();
        if (other.tag.Contains("Enemy"))
        {
            NavMeshAgent nma = other.gameObject.GetComponent<NavMeshAgent>();
            nma.Warp(new Vector3(other.transform.position.x, other.transform.position.y, zPosition));
        }
        else if(other.gameObject.tag.Equals("Player"))
        {
            other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, zPosition);
        }
    }
}
