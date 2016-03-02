using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

    public float currentHealth = 100f;
    public float maximumHealth = 100f;

    private MapGenerator mapGScript;

    void Start()
    {
        mapGScript = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGenerator>();
    }

    void OnDestroy()
    {
        mapGScript.EnemyDead();
    }

    public float GetPercentageHealth()
    {
        return 100 * currentHealth / maximumHealth;
    }

    public void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0f)
        {
            // Enemy is dead
        }
    }
}
