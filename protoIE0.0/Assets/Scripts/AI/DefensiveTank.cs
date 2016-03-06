using UnityEngine;
using System.Collections;

public class DefensiveTank : AITemplate
{
    // Defensive, melee (suit le joueur, quand faible fuit)
    // Defensive, range (reste à porter maximum de ses attaques, quand faible, fuit, si player rapproche enemy s'enfuit)

    public DefensiveTank(Transform enemyTransformHead, Transform playerTransform, NavMeshAgent navAgent, EnemyStats enemyStats, PlayerStats playerStats)
    {
        this.enemyTransformHead = enemyTransformHead;
        this.playerTransform = playerTransform;
        this.navAgent = navAgent;
        this.enemyStats = enemyStats;
        this.playerStats = playerStats;

        this.navAgent.Stop();
    }

    public override void LookAt()
    { // Look at something

        playerDistance = Vector3.Distance(playerTransform.position, enemyTransformHead.position);

        if (IsOutOfView())
        { // The player is too far away from the enemy

        }
        else
        { // The player can be seen by the enemy
            Quaternion turnRotation;

            if (melee && !IsEnemyHealthy())
            { // Enemy is hurt enough to flee and is melee, stop looking at player
                turnRotation = Quaternion.LookRotation(enemyTransformHead.transform.position - playerTransform.position);
            } else
            {  // Enemy is looking at player
                turnRotation = Quaternion.LookRotation(playerTransform.position - enemyTransformHead.transform.position);
            }
            enemyTransformHead.transform.rotation = Quaternion.Slerp(enemyTransformHead.transform.rotation, turnRotation, Time.deltaTime * damping);
        }
    }

    public override void Move()
    { // Move to a destination
        navAgent.destination = playerTransform.position;
    }

    private void Flee()
    { // Flee to a destination
        Vector3 direction = (enemyTransformHead.position - playerTransform.position).normalized;
        navAgent.destination = enemyTransformHead.position + direction * fearFactor;
    }

    public override void Attack()
    { // Attack something

        if (melee)
        { // The enemy is a melee type
          // Do something melee
            playerStats.AffectHP(enemyStats.GetDamage() * -1);
        }
        else
        { // The enemy is a range type
            // Do something at range, like generating bullet and throwing it
        }
    }

    public override void ChaseAndAttack()
    {
        // Debug.Log(playerDistance + " : " + attackRange);

        if (playerDistance <= attackRange)
        { // The player is within attack range

            //Debug.Log("Is within attack : " + playerDistance);

            if (!melee && (playerDistance < (attackRange * thresholdRangeToFlee)))
            { // The enemy is ranged and the player is too near
                //Debug.Log("Fleeing");
                navAgent.Resume();
                Flee();
                Attack();
            } else
            { // The enemy is in range to attack
                //Debug.Log("Stop and Attack");
                navAgent.Stop();
                Attack();
            }
            
        }
        else if (playerDistance <= chaseRange)
        { // The player is within chasing range
            navAgent.Resume();
            Move();
        }
    }
}
