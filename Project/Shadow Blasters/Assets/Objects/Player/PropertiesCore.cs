using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
	/// <summary>
	/// Guarda refer�ncia de Components importados do jogador (rigidbody, colliders, etc)
	/// </summary>
	public class PropertiesCore : BaseMember
	{
		[HideInInspector] public Rigidbody2D Rigidbody;
		[HideInInspector] public BoxCollider2D Collider;
		[HideInInspector] public BoxCollider2D FeetCollider;

        public static GameObject Player;
		public static PropertiesCore s_Instance;

		[HideInInspector] public bool Attacking = false;


		void Awake()
		{
			if (Player == null)
			{
				Player = gameObject;
				s_Instance = this;

				Rigidbody = GetComponent<Rigidbody2D>();
				Collider = GetComponent<BoxCollider2D>();
				FeetCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();

				Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemySolid"), true);
			}
			else
			{
				Destroy(gameObject);
				throw new UnityException("S� � permitida uma inst�ncia do Player por cena");
			}
		}
	}
}
