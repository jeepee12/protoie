using UnityEngine;

public class AITemplateTank
{

    /// <summary>
    /// If the enemy is melee or range type
    /// </summary>
    public bool melee = true;
    /// <summary>
    /// The attack range in unity unit
    /// </summary>
    public float attackRange;
    /// <summary>
    /// The chasing range in unity unit
    /// </summary>
    public float chaseRange;
    /// <summary>
    /// The percentage of damping to rotate
    /// </summary>
    public float damping;
    /// <summary>
    /// The multiplier to the fleeing speed
    /// </summary>
    public float fearFactor;
    /// <summary>
    /// The looking range in unity unit
    /// </summary>
    public float lookRange;
    /// <summary>
    /// The percentage for low health
    /// </summary>
    public float lowHealth;
    /// <summary>
    /// The percentage for the fleeing if enemy is too near in the attack range
    /// </summary>
    public float thresholdRangeToFlee;
    /// <summary>
    /// The distance between the player and the enemy
    /// </summary>
    protected float playerDistance = 0f;

    protected Transform playerTransform;
    protected Transform enemyTransformHead;
    protected Transform enemyTransformLeg;

    protected EnemyStats enemyStats;

    protected NavMeshAgent navAgent;

    /// <summary>
    /// The enemy look at something
    /// </summary>
    public virtual void LookAt() { }

    /// <summary>
    /// The enemy move to a destination
    /// </summary>
    public virtual void Move() { }

    /// <summary>
    /// The enemy attack something
    /// </summary>
    public virtual void Attack() { }

    /// <summary>
    /// The enemy check for chase and attack reach
    /// </summary>
    public virtual void ChaseAndAttack() { }

    /// <summary>
    /// Is the player out of sight for the enemy?
    /// </summary>
    public bool IsOutOfView()
    {
        return playerDistance > lookRange;
    }

    /// <summary>
    /// Is the enemy is healthy enough to fight
    /// </summary>
    public bool IsEnemyHealthy()
    {
        return enemyStats.GetPercentageHealth() > lowHealth;
    }
}
