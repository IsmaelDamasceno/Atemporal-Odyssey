using CrystalBot;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Aspid
{
	public class PropertiesCore : BasePropertiesCore
	{

		[SerializeField] private Vector2 walkTime;
		[SerializeField] private float attackTriggerDistance;

		private Animator animator;
		private EnemyBehaviour behaviour;
		private WallDetection wallDetection;
		private DamageMember damage;
		private Spit spit;

		EnemyState currentState;

		void Start()
		{
			animator = GetComponent<Animator>();
			behaviour = GetComponent<EnemyBehaviour>();

			wallDetection = GetComponentInChildren<WallDetection>();
			damage = GetComponentInChildren<DamageMember>();

			spit = GetComponent<Spit>();

			currentState = EnemyState.Patrol;

			StartCoroutine(AttackCoroutine());
		}

		void Update()
		{

		}

		private IEnumerator AttackCoroutine()
		{
			yield return new WaitForSeconds(Random.Range(walkTime.x, walkTime.y));

			if (Vector3.Distance(transform.position, Player.PropertiesCore.Player.transform.position) <= attackTriggerDistance)
			{
				ChangeState(EnemyState.Attack);
			}
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

#if UNITY_EDITOR
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.white;


			Gizmos.DrawWireSphere(transform.position, attackTriggerDistance);

			Handles.Label(transform.position + Vector3.up, Vector3.Distance(transform.position, Player.PropertiesCore.Player.transform.position).ToString());
		}
#endif
	}
}
