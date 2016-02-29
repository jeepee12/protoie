using UnityEngine;
using System.Collections;

public class AIFollower : AITemplate {

    public AIFollower(Transform enemyTransform, Transform playerTransform, NavMeshAgent navAgent)
    {
        this.enemyTransform = enemyTransform;
        this.playerTransform = playerTransform;
        this.navAgent = navAgent;
    }

    public override void LookAt()
    { // Look at something
        Quaternion turnRotation = Quaternion.LookRotation(this.playerTransform.position - this.enemyTransform.transform.position);

        this.enemyTransform.transform.rotation = Quaternion.Slerp(this.enemyTransform.transform.rotation, turnRotation, Time.deltaTime * this.damping);
    }

    public override void Move()
    { // Move to a destination

        this.navAgent.destination = this.playerTransform.position;
    }

    public override void Attack()
    { // Attack something

    }
}
