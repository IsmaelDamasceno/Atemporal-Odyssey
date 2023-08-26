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

		private MoveMember _moveMember;
		private JumpMember _jumpMember;

		public static GameObject Player;
		public static PropertiesCore s_Instance;

		public static float s_IvulnerableTime = 1f;
		public static bool s_Ivulnerable = false;

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

				_moveMember = GetComponent<MoveMember>();
				_jumpMember = GetComponent<JumpMember>();

				Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("EnemySolid"), true);
			}
			else
			{
				Destroy(gameObject);
				throw new UnityException("Só é permitida uma instância do Player por cena");
			}
		}

		private void Update()
		{
			if (!_moveMember.enabled)
			{
				if (_jumpMember.OnFloor())
				{
					_moveMember.enabled = true;
					_jumpMember.JumpControl = true;
				}
			}
		}

		public static void ApplyForce(Vector2 forceToApply)
		{
			s_Instance._moveMember.enabled = false;
			s_Instance._jumpMember.JumpControl = false;

			s_Instance.transform.position += Vector3.up * 0.1f;
			s_Instance.Rigidbody.velocity = Vector2.zero;
			s_Instance.Rigidbody.AddForce(forceToApply, ForceMode2D.Impulse);
		}

		public static void SetIvulnerable()
		{
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
			s_Ivulnerable = true;
			s_Instance.StartCoroutine(s_Instance.VulnerabilityCoroutine());
		}

		private IEnumerator VulnerabilityCoroutine()
		{
			yield return new WaitForSeconds(s_IvulnerableTime);
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
			s_Ivulnerable = false;
		}
	}
}
