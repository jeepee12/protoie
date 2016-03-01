using UnityEngine;
using System.Collections;

public class AICoward : AITemplate
{
    public AICoward(Transform enemyTransform, Transform playerTransform, NavMeshAgent navAgent)
    {
        this.enemyTransform = enemyTransform;
        this.playerTransform = playerTransform;
        this.navAgent = navAgent;
    }

    public override void LookAt()
    { // Look at something

        Quaternion turnRotation = Quaternion.LookRotation(this.enemyTransform.transform.position - this.playerTransform.position);


        this.enemyTransform.transform.rotation = Quaternion.Slerp(this.enemyTransform.transform.rotation, turnRotation, Time.deltaTime * this.damping);
    }

    public override void Move()
    { // Move to a destination

        Transform startTransform = this.enemyTransform;
        this.enemyTransform.rotation = Quaternion.LookRotation(this.enemyTransform.position - this.playerTransform.position);

        Vector3 runTo = this.enemyTransform.transform.position + this.enemyTransform.transform.forward * this.fearFactor;

        NavMeshHit destination;
        NavMesh.SamplePosition(runTo, out destination, 50, 1 << NavMesh.GetAreaFromName("Walkable"));

        this.enemyTransform.position = startTransform.position;
        this.enemyTransform.rotation = startTransform.rotation;

        this.navAgent.destination = destination.position;
    }

    public override void Attack()
    { // Attack something

    }

}
