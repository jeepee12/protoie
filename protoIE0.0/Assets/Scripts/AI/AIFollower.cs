﻿using UnityEngine;
using System.Collections;

public class AIFollower : AITemplate {

    public AIFollower(Transform enemyTransform, Transform playerTransform, NavMeshAgent navAgent)
    {
        this.enemyTransformHead = enemyTransform;
        this.playerTransform = playerTransform;
        this.navAgent = navAgent;
    }

    public override void LookAt()
    { // Look at something
        Quaternion turnRotation = Quaternion.LookRotation(this.playerTransform.position - this.enemyTransformHead.transform.position);

        this.enemyTransformHead.transform.rotation = Quaternion.Slerp(this.enemyTransformHead.transform.rotation, turnRotation, Time.deltaTime * this.damping);
    }

    public override void Move()
    { // Move to a destination

        this.navAgent.destination = this.playerTransform.position;
    }

    public override void Attack()
    { // Attack something

    }
}
