using UnityEngine;

public class LandObjective : MonoBehaviour {

	private MapGenerator mapGScript;
	
	void Start () {
		mapGScript = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGenerator>();
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			mapGScript.StageIsCompleted();
		}
	}
}
