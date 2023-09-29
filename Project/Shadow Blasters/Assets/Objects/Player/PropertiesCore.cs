using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
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

		[HideInInspector] public static bool ladder = false;
		
        public static GameObject Player;
		public static PropertiesCore s_Instance;

		[HideInInspector] public bool Attacking = false;
		private static Animator animator;
		private static InputMember inputMember;

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
				inputMember = GetComponent<InputMember>();

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
			yield return new WaitForSeconds(.25f);
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
		private void OnTriggerStay2D(Collider2D collision)
		{
			if (!ladder && collision.gameObject.layer == LayerMask.NameToLayer("Ladder"))
			{
				if (inputMember.LadderInput > 0f || (inputMember.LadderInput < 0f && JumpMember.grounded))
				{
					EnterLadder(collision);
				}
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
			else if (collision.gameObject.layer == LayerMask.NameToLayer("Ladder"))
			{
				ExitLadder();
			}
		}

		public static void EnterLadder(Collider2D collision)
		{
			ladder = true;
			s_Instance.Collider.excludeLayers = (int)Mathf.Pow(2, LayerMask.NameToLayer("Ground"));
			s_Instance.GetComponent<LadderMember>().enabled = true;
			s_Instance.GetComponent<LadderMember>().StartLadder(new Vector2(collision.transform.position.x, collision.ClosestPoint(s_Instance.transform.position).y));
			s_Instance.GetComponent<MoveMember>().enabled = false;
		}
		public static void ExitLadder()
		{
			ladder = false;
			s_Instance.Collider.excludeLayers = 0;
			s_Instance.GetComponent<LadderMember>().enabled = false;
			s_Instance.GetComponent<MoveMember>().enabled = true;
		}
	}
}
