using UnityEngine;

public class AITemplate
{

    public bool melee = true;
    public float lookRange = 1000f;
    public float attackRange = 10f;
    public float chaseRange = 300f;
    public float damping = 0.8f;
    public float fearFactor = 50f;
    public float playerDistance = 0f;

    public Transform playerTransform;
    public Transform enemyTransform;

    public NavMeshAgent navAgent;

    public virtual void LookAt() { } // Look at something

    public virtual void Move() { } // Move to a destination

    public virtual void Attack() { } // Attack something

    public virtual void ChaseAndAttack() { } // Check for chase and attack reach

    public bool IsOutOfView()
    {
        return playerDistance > lookRange;
    }
}
