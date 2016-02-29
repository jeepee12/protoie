using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnemySelection
{
    Default, Follower, Coward
}

public class EnemyAI : MonoBehaviour
{

    public EnemySelection enemySelection;
    public AITemplate enemyAI;

    private void Start()
    {
        switch (enemySelection)
        {
            case EnemySelection.Default: 
                enemyAI = new AIFollower(transform, GameObject.FindGameObjectWithTag("Player").transform, GetComponent<NavMeshAgent>());
                break;
            case EnemySelection.Follower:
                enemyAI = new AIFollower(transform, GameObject.FindGameObjectWithTag("Player").transform, GetComponent<NavMeshAgent>());
                break;
            case EnemySelection.Coward:
                enemyAI = new AICoward(transform, GameObject.FindGameObjectWithTag("Player").transform, GetComponent<NavMeshAgent>());
                break;
        }

        enemyAI.enemyTransform = transform;
        enemyAI.playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAI.navAgent = GetComponent<NavMeshAgent>();
    }


    private void FixedUpdate()
    {
        enemyAI.playerDistance = Vector3.Distance(enemyAI.playerTransform.position, transform.position);

        if (enemyAI.playerDistance > enemyAI.lookRange)
        { // The player is too far away from the enemy
            //Debug.Log("The player is too far away from the enemy");

        } else
        { // The player can be seen by the enemy
            //Debug.Log("The player can be seen by the enemy");
            enemyAI.LookAt();
        }
        
        if (enemyAI.playerDistance <= enemyAI.attackRange)
        { // The player is within attack range
            //Debug.Log("The player is within attack range");
            enemyAI.navAgent.Stop();
            enemyAI.Attack();
        }
        else if (enemyAI.playerDistance <= enemyAI.chaseRange)
        { // The player is within chasing range
            //Debug.Log("The player is within chasing range");
            enemyAI.navAgent.Resume();
            enemyAI.Move();
        }
    }

}