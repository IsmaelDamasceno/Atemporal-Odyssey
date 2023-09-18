using CrystalBot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CrstalBot
{
	public class PropertiesCore : BasePropertiesCore
	{
		public EnemyState currentState;
		private EnemyBehaviour behaviour;
		private WallDetection wallDetection;
		private DamageMember damage;

		void Start()
		{
			behaviour = GetComponent<EnemyBehaviour>();

			wallDetection = GetComponentInChildren<WallDetection>();
			damage = GetComponentInChildren<DamageMember>();

			currentState = EnemyState.Patrol;
		}

		void Update()
		{

		}

		public override void ChangeState(EnemyState newState)
		{
			currentState = newState;

			switch (currentState)
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
			}
		}
	}
}
