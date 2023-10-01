using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
	public class DamageMember : MonoBehaviour, IDamage
	{
		public static DamageMember s_Instance;

		private MoveMember _moveMember;
		private JumpMember _jumpMember;
		private Rigidbody2D _rb;
		private Animator _animator;

		public static float s_IvulnerableTime = 1.25f;
		public static float s_ImpactUncontrolTime = 0.25f;
		public static bool s_Ivulnerable;

		void Awake()
		{
			if (s_Instance == null)
			{
				s_Instance = this;

				_moveMember = GetComponent<MoveMember>();
				_jumpMember = GetComponent<JumpMember>();
				_animator = GetComponent<Animator>();
				_rb = GetComponent<Rigidbody2D>();
				s_Ivulnerable = false;
			}
			else
			{
				Destroy(gameObject);
			}
		}

		void Update()
		{
			if (!_moveMember.enabled && _animator.GetBool("Damaged"))
			{
				if (_jumpMember.OnFloor())
				{
					_moveMember.enabled = true;
					_jumpMember.JumpControl = true;
					_animator.SetBool("Damaged", false);
				}
			}
		}

		public static void ApplyForce(Vector2 forceToApply)
		{
			s_Instance._moveMember.enabled = false;
			s_Instance._jumpMember.JumpControl = false;
			s_Instance._animator.SetBool("Damaged", true);

			s_Instance.transform.position += Vector3.up * 0.1f;
			s_Instance._rb.velocity = Vector2.zero;
			s_Instance._rb.AddForce(forceToApply, ForceMode2D.Impulse);

			s_Instance.StartCoroutine(s_Instance.ImpactCoroutine());
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

		private IEnumerator ImpactCoroutine()
		{
			yield return new WaitForSeconds(s_ImpactUncontrolTime);
			_moveMember.enabled = true;
			_jumpMember.JumpControl = true;
			_animator.SetBool("Damaged", false);
		}

		public void ApplyDamage(Transform hitTransform, Vector2 impact, int amount)
		{
			if (s_Ivulnerable)
			{
				return;
			}

			int direction = Math.Sign(transform.position.x - hitTransform.position.x);

			ApplyForce(new Vector2(impact.x * direction, impact.y));
			HealthSystem.ChangeHealth(-amount);
			SetIvulnerable();

			if (TryGetComponent<FlashWhite>(out var component))
			{
				component.EndComponent();
			}
			gameObject.AddComponent<FlashWhite>().Init(s_IvulnerableTime, 0.15f, GetComponent<SpriteRenderer>());
		}
	}
}
