using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    private NavMeshAgent agent = null;
    private float timeOfLastAttack = 0;
    private bool hasStopped = false;
    private ZombieStats stats = null;
    [SerializeField] private Transform target;
    private Vector3 walkPoint;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<ZombieStats>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }
    private void MoveToTarget()
    {
        agent.SetDestination(target.position);

        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (distanceToTarget <= agent.stoppingDistance)
        {
            if (!hasStopped)
            {
                hasStopped = true;
                timeOfLastAttack = Time.time;

            }
            if (Time.time >= timeOfLastAttack + stats.attackSpeed)
            {
                timeOfLastAttack = Time.time;

                Attack();
            }
        }
        else
        {
            if (hasStopped)
            {
                hasStopped = false;
            }
        }


    }

    private void Attack()
    {

    }

    private void Patrol()
    {
        float randomZ = Random.Range(-50, 50);
        float randomX = Random.Range(-50, 50);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        float distanceToWalk = Vector3.Distance(walkPoint, transform.position);

        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget > 5)
        {
            agent.SetDestination(walkPoint);

        }
        else
        {
            MoveToTarget();
        }



    }


}

