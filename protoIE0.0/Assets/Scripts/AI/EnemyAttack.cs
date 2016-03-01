using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public float lookRange = 1000f;
    public float chaseRange = 300f;
    public float attackRange = 10f;
    public float damping = 0.8f;
    public Transform playerTransform;
    public Transform enemyTransform;
    public float playerDistance = 0f;
    public NavMeshAgent navAgent;

    public virtual void LookAt() { } // Look at something

    public virtual void Move() { } // Move to a destination

    public virtual void Attack() { } // Attack something
}

