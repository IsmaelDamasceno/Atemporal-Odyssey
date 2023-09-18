using CrystalBot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aspid
{
	public class PropertiesCore : BasePropertiesCore
	{

		[SerializeField] private Vector2 walkTime;

		private Animator animator;
		private EnemyBehaviour behaviour;
		private WallDetection wallDetection;
		private DamageMember damage;

		EnemyState currentState;

		void Start()
		{
			animator = GetComponent<Animator>();
			behaviour = GetComponent<EnemyBehaviour>();

			wallDetection = GetComponentInChildren<WallDetection>();
			damage = GetComponentInChildren<DamageMember>();

			currentState = EnemyState.Patrol;

			StartCoroutine(AttackCoroutine());
		}

		void Update()
		{

		}

		private IEnumerator AttackCoroutine()
		{
			yield return new WaitForSeconds(Random.Range(walkTime.x, walkTime.y));

			ChangeState(EnemyState.Attack);
			StartCoroutine(AttackCoroutine());
		}

		public override void ChangeState(EnemyState newState)
		{
			currentState = newState;
			switch(currentState)
			{
				case EnemyState.Patrol:
					{
						behaviour.enabled = true;
						wallDetection.enabled = true;
						damage.enabled = true;
					}
					break;
				case EnemyState.Damage:
					{
						behaviour.enabled = false;
						wallDetection.enabled = false;
						damage.enabled = false;
					}
					break;
				case EnemyState.Attack:
					{
						animator.SetTrigger("Spit");
						behaviour.enabled = false;
					}
					break;
			}
		}
	}
}
