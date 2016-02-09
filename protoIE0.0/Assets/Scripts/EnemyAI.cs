using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float lookAtDistance = 100f;
    public float chaseAtDistance = 30f;
    public float attackRange = 10f;
    public float speed = 16f;
    public float damping = 0.8f;

    private float playerDistance;
    private NavMeshAgent navAgent;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }


    private void FixedUpdate()
    {
        playerDistance = Vector3.Distance(target.position, transform.position);

        if (playerDistance > lookAtDistance)
        {
            // Chill for the moment
        } else
        {
            LookAt();
        }
        
        if (playerDistance <= attackRange)
        {
            navAgent.Stop();
            // Enemy do an attack
        }
        else if (playerDistance <= chaseAtDistance)
        {
            navAgent.Resume();
            Move();
        }
    }

    private void LookAt()
    {
        // Rotate to look at player.
        Quaternion turnRotation = Quaternion.LookRotation(target.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, turnRotation, Time.deltaTime * damping);
    }

    private void Move()
    {
        navAgent.destination = target.position;
    }


}