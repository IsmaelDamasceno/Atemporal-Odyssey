using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
	public class DamageMember : MonoBehaviour
	{
		private static DamageMember s_instance;

		private MoveMember _moveMember;
		private JumpMember _jumpMember;
		private Rigidbody2D _rb;

		public static float s_IvulnerableTime = 1.25f;
		public static float s_ImpactUncontrolTime = 0.25f;
		public static bool s_Ivulnerable = false;

		void Awake()
		{
			if (s_instance == null)
			{
				s_instance = this;

				_moveMember = GetComponent<MoveMember>();
				_jumpMember = GetComponent<JumpMember>();
				_rb = GetComponent<Rigidbody2D>();
			}
			else
			{
				Destroy(gameObject);
			}
		}

		void Update()
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
			s_instance._moveMember.enabled = false;
			s_instance._jumpMember.JumpControl = false;

			s_instance.transform.position += Vector3.up * 0.1f;
			s_instance._rb.velocity = Vector2.zero;
			s_instance._rb.AddForce(forceToApply, ForceMode2D.Impulse);

			s_instance.StartCoroutine(s_instance.ImpactCoroutine());
		}

		public static void SetIvulnerable()
		{
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
			s_Ivulnerable = true;
			s_instance.StartCoroutine(s_instance.VulnerabilityCoroutine());
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
		}
	}
}
