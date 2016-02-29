using UnityEngine;

public class BoundingLimitLeft : MonoBehaviour {

    private float xPosition;

    public void Start()
    {
        xPosition = transform.position.x * -1 - transform.localScale.x*2;
    }

    void OnTriggerEnter(Collider other)
    {
        other.transform.position = new Vector3(xPosition, other.transform.position.y, other.transform.position.z);
    }
}
