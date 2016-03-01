using UnityEngine;


// TODO : Defensive, melee (suit le joueur, quand faible fuit)
// TODO : Defensive, range (reste à porter maximum de ses attaques, quand faible, fuit, si player rapproche enemy s'enfuit)

public enum EnemySelection
{
    Default, Offensive, Defensive
}

public class EnemyAI : MonoBehaviour
{
    private AITemplate enemyAI;
    public EnemySelection enemySelection;

    public bool melee = true;
    public float attackRange = 10f;
    public float chaseRange = 300f;
    public float damping = 0.8f;
    public float fearFactor = 50f;
    public float lookRange = 1000f;

    private void Start()
    {

        if (enemySelection == EnemySelection.Offensive)
        { // Offensive behavior
            enemyAI = new Offensive(transform, GameObject.FindGameObjectWithTag("Player").transform, GetComponent<NavMeshAgent>());

            enemyAI.melee = melee;
            enemyAI.attackRange = attackRange;
            enemyAI.chaseRange = chaseRange;
            enemyAI.damping = damping;
            enemyAI.fearFactor = fearFactor;
            enemyAI.lookRange = lookRange;
        }
        else if (enemySelection == EnemySelection.Defensive)
        { // Defensive behavior

        }
        else
        { // Default AI, Offensive Melee
            enemyAI = new Offensive(transform, GameObject.FindGameObjectWithTag("Player").transform, GetComponent<NavMeshAgent>());

            enemyAI.melee = melee;
            enemyAI.attackRange = attackRange;
            enemyAI.chaseRange = chaseRange;
            enemyAI.damping = damping;
            enemyAI.fearFactor = fearFactor;
            enemyAI.lookRange = lookRange;
        }

        enemyAI.enemyTransform = transform;
        enemyAI.playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAI.navAgent = GetComponent<NavMeshAgent>();
    }


    private void FixedUpdate()
    {
        enemyAI.playerDistance = Vector3.Distance(enemyAI.playerTransform.position, transform.position);

        enemyAI.LookAt();

        enemyAI.ChaseAndAttack();
    }

}