using UnityEngine;
using System.Collections;

public class OffensiveTank : AITemplate
{
    // Offensive, melee (suit toujours le joueur)
    // Offensive, range (suit toujours le joueur, mais garde une distance)
    private float attackStart;

    public OffensiveTank(Transform enemyTransformHead, Transform playerTransform, NavMeshAgent navAgent, EnemyStats enemyStats, PlayerStats playerStats)
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
        {// The player can be seen by the enemy
            Quaternion turnRotation = Quaternion.LookRotation(playerTransform.position - enemyTransformHead.transform.position);
            enemyTransformHead.transform.rotation = Quaternion.Slerp(enemyTransformHead.transform.rotation, turnRotation, Time.deltaTime * damping);
        }
    }

    public override void Move()
    { // Move to a destination
        navAgent.destination = playerTransform.position;
    }

    public override void Attack()
    { // Attack something
        
        if (melee && Time.time > attackStart + enemyStats.attackSpeed)
        { // The enemy is a melee type
            // Do something melee
            playerStats.AffectHP(enemyStats.GetDamage() * -1);
            attackStart = Time.time;
        } else
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
