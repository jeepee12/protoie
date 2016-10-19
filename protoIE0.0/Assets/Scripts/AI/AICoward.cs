using UnityEngine;
using System.Collections;

public class AICoward : AITemplate
{
    public AICoward(Transform enemyTransform, Transform playerTransform, NavMeshAgent navAgent)
    {
        this.enemyTransformHead = enemyTransform;
        this.playerTransform = playerTransform;
        this.navAgent = navAgent;
    }

    public override void LookAt()
    { // Look at something

        Quaternion turnRotation = Quaternion.LookRotation(this.enemyTransformHead.transform.position - this.playerTransform.position);
        this.enemyTransformHead.transform.rotation = Quaternion.Slerp(this.enemyTransformHead.transform.rotation, turnRotation, Time.deltaTime * this.damping);
    }

    public override void Move()
    { // Move to a destination

        Transform startTransform = this.enemyTransformHead;
        this.enemyTransformHead.rotation = Quaternion.LookRotation(this.enemyTransformHead.position - this.playerTransform.position);

        Vector3 runTo = this.enemyTransformHead.transform.position + this.enemyTransformHead.transform.forward * this.fearFactor;

        NavMeshHit destination;
        NavMesh.SamplePosition(runTo, out destination, 50, 1 << NavMesh.GetAreaFromName("Walkable"));

        this.enemyTransformHead.position = startTransform.position;
        this.enemyTransformHead.rotation = startTransform.rotation;

        this.navAgent.destination = destination.position;
    }

    public override void Attack()
    { // Attack something

    }

}
