using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
	/// <summary>
	/// Guarda referência de Components importados do jogador (rigidbody, colliders, etc)
	/// </summary>
	public class PropertiesCore : BaseMember
	{
		[HideInInspector] public Rigidbody2D Rigidbody;
		[HideInInspector] public BoxCollider2D Collider;
		[HideInInspector] public BoxCollider2D FeetCollider;
		

        public static GameObject Player;
		public static PropertiesCore s_Instance;

		[HideInInspector] public bool Attacking = false;
		private static Animator animator;

		void Awake()
		{
			if (Player == null)
			{
				Player = gameObject;
				s_Instance = this;

				Rigidbody = GetComponent<Rigidbody2D>();
				Collider = GetComponent<BoxCollider2D>();
				FeetCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
				animator = GetComponent<Animator>();

				Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemySolid"), true);

				DontDestroyOnLoad(gameObject);

				GameController.SavePlayerInitialPos(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
		}

		private void OnTriggerStay2D(Collider2D collision)
		{
			Debug.Log(LayerMask.LayerToName(collision.gameObject.layer));
			if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
			{
				animator.SetBool("Swimming", true);
				animator.Play("Base Layer.Swim");
			}
		}
	}
}
