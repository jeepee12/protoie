using UnityEngine;

public enum EnemySelection
{
    Default, OffensiveTank, DefensiveTank//, Offensive, Defensive
}

public class EnemyAI : MonoBehaviour
{
    private AITemplate enemyAI = null;
    public EnemySelection enemySelection;
    /// <summary>
    /// If the enemy is melee or range type
    /// </summary>
    public bool melee = true;
    /// <summary>
    /// The attack range in unity unit
    /// </summary>
    public float attackRange = 10f;
    /// <summary>
    /// The chasing range in unity unit
    /// </summary>
    public float chaseRange = 300f;
    /// <summary>
    /// The percentage of damping to rotate
    /// </summary>
    public float damping = 0.9f;
    /// <summary>
    /// The multiplier to the fleeing speed
    /// </summary>
    public float fearFactor = 50f;
    /// <summary>
    /// The looking range in unity unit
    /// </summary>
    public float lookRange = 1000f;
    /// <summary>
    /// The percentage for low health
    /// </summary>
    public float lowHealth = 10f;
    /// <summary>
    /// The percentage for the fleeing if enemy is too near in the attack range
    /// </summary>
    public float thresholdRangeToFlee = 0.7f;

    private void Start()
    {

        Transform enemyTransformHead = this.gameObject.transform.GetChild(0);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Transform playerTransform = player.transform;
        NavMeshAgent navAgent = GetComponentInChildren<NavMeshAgent>();
        EnemyStats enemyStats = this.gameObject.GetComponent<EnemyStats>();

        PlayerStats playerStatsScript = player.GetComponent<PlayerStats>();

        if (enemySelection == EnemySelection.OffensiveTank)
        { // Offensive Tank behavior
            enemyAI = new OffensiveTank(enemyTransformHead, playerTransform, navAgent, enemyStats, playerStatsScript);
        }
        else if (enemySelection == EnemySelection.DefensiveTank)
        { // Defensive Tank behavior
            enemyAI = new DefensiveTank(enemyTransformHead, playerTransform, navAgent, enemyStats, playerStatsScript);
        }
        else
        { // Default AI, Offensive Tank
            enemyAI = new OffensiveTank(enemyTransformHead, playerTransform, navAgent, enemyStats, playerStatsScript);
        }

        enemyAI.melee = melee;
        enemyAI.attackRange = attackRange;
        enemyAI.chaseRange = chaseRange;
        enemyAI.damping = damping;
        enemyAI.fearFactor = fearFactor;
        enemyAI.lookRange = lookRange;
        enemyAI.lowHealth = lowHealth;
        enemyAI.thresholdRangeToFlee = thresholdRangeToFlee;

    }


    private void FixedUpdate()
    {
        enemyAI.LookAt();
        enemyAI.ChaseAndAttack();
    }

}