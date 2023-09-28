using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

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

		[HideInInspector] public static bool swimming = false;
		[HideInInspector] public static bool swimJump = false;
		private static Coroutine swimJumpCoroutine;
		
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

		private IEnumerator SwimJumpCoroutine()
		{
			swimJump = false;
			yield return new WaitForSeconds(1f);
			if (swimming)
			{
				swimJump = true;
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
			{
				GetComponent<JumpMember>()._startedJump = false;

				animator.SetBool("Swimming", true);
				animator.Play("Base Layer.Swim");
				swimming = true;
				StartCoroutine(SwimJumpCoroutine());
			}
		}
		private void OnTriggerExit2D(Collider2D collision)
		{
			if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
			{
				animator.SetBool("Swimming", false);
				swimming = false;
				swimJump = false;
			}
		}
	}
}
