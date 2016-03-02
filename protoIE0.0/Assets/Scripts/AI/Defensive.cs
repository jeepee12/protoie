using UnityEngine;
using System.Collections;

public class Defensive : AITemplate
{
    // Defensive, melee (suit le joueur, quand faible fuit)
    // Defensive, range (reste à porter maximum de ses attaques, quand faible, fuit, si player rapproche enemy s'enfuit)

    public Defensive(Transform enemyTransform, Transform playerTransform, NavMeshAgent navAgent, EnemyStats enemyStats)
    {
        this.enemyTransformHead = enemyTransform;
        this.playerTransform = playerTransform;
        this.navAgent = navAgent;
        this.enemyStats = enemyStats;
    }

    public override void LookAt()
    { // Look at something

        playerDistance = Vector3.Distance(playerTransform.position, enemyTransformHead.position);

        if (IsOutOfView())
        { // The player is too far away from the enemy
            
        }
        else
        {// The player can be seen by the enemy

            if (IsEnemyHealthy() && melee)
            { // Enemy is healthy enough to fight and is melee
                Quaternion turnRotation = Quaternion.LookRotation(playerTransform.position - enemyTransformHead.transform.position);
                enemyTransformHead.transform.rotation = Quaternion.Slerp(enemyTransformHead.transform.rotation, turnRotation, Time.deltaTime * damping);
            } else if (!IsEnemyHealthy() && melee)
            { // Enemy is hurt enough to flee and is melee
                
            }
            else if (IsEnemyHealthy() && !melee)
            { // The enemy is healthy enough to fight, is ranged

            } else
            {

            }

            
        }
    }

    public override void Move()
    { // Move to a destination
        navAgent.destination = playerTransform.position;
    }

    public override void Attack()
    { // Attack something

        if (melee)
        { // The enemy is a melee type
            // Do something melee
        }
        else
        { // The enemy is a range type
            // Do something at range, like generating bullet and throwing it
        }
    }

    public override void ChaseAndAttack()
    {
        if (playerDistance <= attackRange)
        { // The player is within attack range
            navAgent.Stop();
            Attack();
        }
        else if (playerDistance <= chaseRange)
        { // The player is within chasing range
            navAgent.Resume();
            Move();
        }
    }
}
