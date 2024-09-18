using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour
{
	private float movementSpeed, distance, idleTimer_Script = 3;
	public float editor_WalkSpeed = 1f, editor_RunSpeed = 2f, maxDistance = 5, idleTimer = 3;

	[SerializeField] private float walkingRange = 50;

	[SerializeField] private NavMeshAgent agent;
	private Animator enemyAnimator;

	[SerializeField] private EnemyState currentState;
	[SerializeField] private Transform player;
	public bool isRunning;

	private bool pullRandomPath = true;

	private void Start()
	{
		idleTimer_Script = idleTimer;
		pullRandomPath = true;
		currentState = EnemyState.Passive;
	}

	private void Update()
	{
		EnemyBehaviour();
		MovementLogic();
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
		if (agent.hasPath == false)
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
					return;
				}
			}
		}
	}

	private void RoamingBehaviour()
	{
		if (agent.hasPath == false) agent.SetDestination(RandomPosition());
		else currentState = EnemyState.Passive;
	}

	private void ChaseBehaviour()
	{
		agent.SetDestination(player.transform.position);

		if(FieldOfView.canSeePlayer == false)
		{
			currentState = EnemyState.Passive;
			isRunning = false;
		}
	}

	private void MovementLogic()
	{
		agent.speed = movementSpeed;

		if (FieldOfView.canSeePlayer == true)
		{
			currentState = EnemyState.Chase;
			isRunning = true;
		}

		if (isRunning) movementSpeed = editor_RunSpeed;
		else if (!isRunning && agent.hasPath == true) movementSpeed = editor_WalkSpeed;
		else if(!isRunning && agent.hasPath == false) agent.speed = 0;
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