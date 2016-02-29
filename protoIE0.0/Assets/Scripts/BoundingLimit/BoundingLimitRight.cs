using UnityEngine;

public class BoundingLimitRight : MonoBehaviour {
    private float xPosition;
    public GameObject mapGenerator;
    private MapGenerator mapGScript;

    private void Start()
    {
        mapGScript = mapGenerator.GetComponent<MapGenerator>();
        xPosition = transform.position.x * -1 + transform.localScale.x*2;
    }

    void OnTriggerEnter(Collider other)
    {
        mapGScript.Collision();
        other.transform.position = new Vector3(xPosition, other.transform.position.y, other.transform.position.z);
    }
}
