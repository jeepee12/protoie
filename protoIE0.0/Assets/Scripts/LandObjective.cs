using UnityEngine;

public class LandObjective : MonoBehaviour {

	private MapGenerator mapGScript;
    private PlayerStats player;
    private float playerEntryTime;
    public float healCoolDown = 2000.0f;
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
            if(Time.time - playerEntryTime > healCoolDown)
            {
                player.AffectHP(healAmount);
                playerEntryTime = Time.time;
            }
        }
    }
}
