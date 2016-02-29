using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

    private MapGenerator mapGScript;

    void Start()
    {
        mapGScript = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGenerator>();
    }

    void OnDestroy()
    {
        Debug.Log("Enemy Killed");
        mapGScript.EnemyDead();
    }
}
