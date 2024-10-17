using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private float movementSpeed, idleTimer_Script = 3, enemyAttackRange, script_DefendTimer = 3;
    public float editor_WalkSpeed = 1f, editor_RunSpeed = 2f, maxDistance = 5, idleTimer = 3, enemyHP = 100, stoppingDistance = 0.5f, editor_DefendTimer = 3;
    public static float enemyHP_Static;
    public float despawnTimer = 5;
    [SerializeField] private float walkingRange = 50;
    [SerializeField] private float turnSpeed = 5f; // Snelheid van draaien richting de speler

    [SerializeField] private NavMeshAgent agent;
    public Animator enemyAnimator;

    [SerializeField] private EnemyState currentState;
    [SerializeField] private Transform player;
    public bool isRunning, isHitted = false;
    public static bool hasDied = false;

    private bool pullRandomPath = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        idleTimer_Script = idleTimer;
        pullRandomPath = true;
        currentState = EnemyState.Passive;
        hasDied = false;
        isHitted = false;
        enemyHP_Static = enemyHP;
    }

    private void Update()
    {
        if (!hasDied)
        {
            EnemyBehaviour();
            MovementLogic();
        }
        HPBehaviour();
    }

    private void HPBehaviour()
    {
        if (enemyHP <= 0)
        {
            hasDied = true;
            DisablePhysicsDrivenAnimation();

            if (Identity.ID_Static == 0)
                Destroy(gameObject);
            else if (Identity.ID_Static == 1)
            {
                agent.enabled = false;
                enemyAnimator.enabled = false;
                //GetComponent<Rigidbody>().isKinematic = false;
                despawnTimer -= Time.deltaTime;
                if (despawnTimer <= 0) Destroy(gameObject);
            }
        }
    }

    // Method to disable PhysicsDrivenAnimation on the GameObject and its children
    private void DisablePhysicsDrivenAnimation()
    {
        // Disable on the main GameObject
        PhysicsDrivenAnimation physicsDrivenAnimation = GetComponent<PhysicsDrivenAnimation>();
        if (physicsDrivenAnimation != null)
        {
            physicsDrivenAnimation.enabled = false;
        }

        // Disable on all children GameObjects
        PhysicsDrivenAnimation[] childPhysicsDrivenAnimations = GetComponentsInChildren<PhysicsDrivenAnimation>();
        foreach (PhysicsDrivenAnimation childAnimation in childPhysicsDrivenAnimations)
        {
            childAnimation.enabled = false;
        }
    }

    private void EnemyBehaviour()
    {
        switch (currentState)
        {
            case EnemyState.Passive:
                PassiveState();
                break;
            case EnemyState.Roaming:
                RoamingBehaviour();
                break;
            case EnemyState.Chase:
                ChaseBehaviour();
                break;
            default:
                break;
        }
    }

    private void PassiveState()
    {
        if (!agent.hasPath)
        {
            if (pullRandomPath)
            {
                currentState = EnemyState.Roaming;
                pullRandomPath = false;
            }
            else
            {
                idleTimer_Script -= Time.deltaTime;
                if (idleTimer_Script <= 0)
                {
                    pullRandomPath = true;
                    idleTimer_Script = idleTimer;
                }
            }
        }
    }

    private void RoamingBehaviour()
    {
        if (!agent.hasPath)
            agent.SetDestination(RandomPosition());
        else
            currentState = EnemyState.Passive;
    }

    private void ChaseBehaviour()
    {
        enemyAttackRange = Vector3.Distance(agent.transform.position, player.position);
        agent.SetDestination(player.transform.position);

        if (!FieldOfView.canSeePlayer && enemyAttackRange > stoppingDistance)
        {
            agent.stoppingDistance = 0;
            currentState = EnemyState.Passive;
            isRunning = false;
        }
    }

    private void MovementLogic()
    {
        if (enemyAnimator != null)
        {
            enemyAnimator.SetFloat("velocity", agent.velocity.magnitude);

            if (enemyAttackRange < stoppingDistance && isRunning)
            {
                enemyAnimator.SetBool("isAttacking", true);
                SmoothRotateTowards(player.position);

                if (isHitted)
                {
                    enemyAnimator.SetBool("isDefending", true);
                    script_DefendTimer -= Time.deltaTime;
                    if (script_DefendTimer <= 0)
                    {
                        enemyAnimator.SetBool("isDefending", false);
                        script_DefendTimer = editor_DefendTimer;
                        isHitted = false;
                    }
                }
            }
            else
            {
                enemyAnimator.SetBool("isAttacking", false);
                if (isHitted)
                {
                    enemyAnimator.SetBool("isDefending", false);
                    script_DefendTimer = editor_DefendTimer;
                    isHitted = false;
                }
            }
        }

        agent.speed = movementSpeed;

        if (FieldOfView.canSeePlayer)
        {
            agent.stoppingDistance = stoppingDistance;
            currentState = EnemyState.Chase;
            isRunning = true;
        }

        if (isRunning)
            movementSpeed = editor_RunSpeed;
        else if (!isRunning && agent.hasPath)
            movementSpeed = editor_WalkSpeed;
        else if (!isRunning && !agent.hasPath)
            agent.speed = 0;
    }

    private void SmoothRotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private Vector3 RandomPosition()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * walkingRange;
        randomDirection += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkingRange, NavMesh.AllAreas);

        return hit.position;
    }

    private enum EnemyState
    {
        Passive,
        Roaming,
        Chase
    }
}
