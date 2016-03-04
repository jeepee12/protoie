using UnityEngine;

public class LandObjective : MonoBehaviour {

	private MapGenerator mapGScript;
    private PlayerStats player;
    private float playerEntryTime;
    public float healCoolDown = 2;
    public int healAmount = 1;
	
	void Start () {
		mapGScript = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGenerator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
            playerEntryTime = 0;
            mapGScript.StageIsCompleted();
        }

	}

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            playerEntryTime = Time.time;

            //Debug.Log("TriggerEnter" + player.getHP());
        }
    }

    void Update()
    {
        if(playerEntryTime > 0)
        {
            //Debug.Log("before healing" + playerEntryTime + "time" + Time.time);
            if (Time.time - playerEntryTime > healCoolDown)
            {
                //Debug.Log("Healing time" + playerEntryTime);
                player.AffectHP(healAmount);
                playerEntryTime = Time.time;
            }
        }
    }
}
