﻿using UnityEngine;
using System.Collections;

public class Offensive : AITemplate
{

    // Offensive, melee (suit toujours le joueur)
    // Offensive, range (suit toujours le joueur, mais garde une distance)

    public Offensive(Transform enemyTransform, Transform playerTransform, NavMeshAgent navAgent)
    {
        this.enemyTransform = enemyTransform;
        this.playerTransform = playerTransform;
        this.navAgent = navAgent;
    }
    
    public override void LookAt()
    { // Look at something

        playerDistance = Vector3.Distance(playerTransform.position, enemyTransform.position);

        if (IsOutOfView())
        { // The player is too far away from the enemy
        }
        else
        {// The player can be seen by the enemy
            Quaternion turnRotation = Quaternion.LookRotation(playerTransform.position - enemyTransform.transform.position);
            enemyTransform.transform.rotation = Quaternion.Slerp(enemyTransform.transform.rotation, turnRotation, Time.deltaTime * damping);
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