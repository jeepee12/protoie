using UnityEngine;

public class BoundingLimitBottom: MonoBehaviour {

    private float zPosition;

    public void Start()
    {
        zPosition = transform.position.z * -1 - transform.localScale.z*2;
    }

    void OnTriggerEnter(Collider other)
    {
        other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, zPosition);
    }
}
