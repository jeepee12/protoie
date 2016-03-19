using UnityEngine;

public class LandObjective : MonoBehaviour {

	private MapGenerator mapGScript;
    private PlayerStats player;
    private float playerEntryTime;
    public float healCoolDown = 2;
    public int healAmount = 1;
    public bool port;
	
	void Start () {
		mapGScript = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGenerator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            if(port)
            {
                player.AffectHP(healAmount);
                playerEntryTime = Time.time - healCoolDown;
            }

            mapGScript.StageIsCompleted();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (port)
            {
                if (Time.time > playerEntryTime + healCoolDown)
                {
                    //Debug.Log("Healing time" + playerEntryTime);
                    player.AffectHP(healAmount);
                    playerEntryTime = Time.time;
                }
            }
        }
    }
}
