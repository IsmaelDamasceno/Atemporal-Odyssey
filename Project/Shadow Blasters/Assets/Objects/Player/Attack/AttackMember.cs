using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
	/// <summary>
	/// Controla o Slash ao atacar
	/// </summary>
	public class AttackMember : MonoBehaviour
	{
		public bool CanAttack { get; set; }

		private RestrictProp<bool> _attackingProp = new RestrictProp<bool>(false, typeof(AttackMember), typeof(FinishAttack));
		public bool GetAttacking()
		{
			return _attackingProp.Value;
		}

		public bool SetAttacking(bool value, Type classType)
		{
			bool couldSet = _attackingProp.TrySet(value, classType);
			if (couldSet)
			{
				transform.parent.GetComponent<PropertiesCore>().Attacking = _attackingProp.Value;
			}
			return couldSet;
		}

		[SerializeField] private float _damage;
		public float Cooldown;
		private bool _attackingLastFrame;

		private SpriteRenderer _sprRenderer;

		private Animator _selfAnimator;
		private Animator _playerAnimator;

		private InputMember _inputMember;

		void Awake()
		{
			transform.parent.GetComponent<PropertiesCore>().RegisterMember("Attack", this);
			#region Animation Setup
			_sprRenderer = GetComponent<SpriteRenderer>();
			_selfAnimator = GetComponent<Animator>();
			_playerAnimator = transform.parent.GetComponent<Animator>();

			CanAttack = true;
			#endregion

			#region Attack Controls
			_inputMember = transform.parent.GetComponent<InputMember>();
			#endregion
		}

		private void Update()
		{
			bool attacking = _inputMember.AttackInput;
			if (attacking && !_attackingLastFrame)
			{
				Attack();
			}
			_attackingLastFrame = attacking;

			if (_inputMember.MoveInput != 0f)
			{
				_sprRenderer.flipX = (_inputMember.MoveInput < 0f);
			}
		}

		private IEnumerator CooldownWait()
		{
			yield return new WaitForSeconds(Cooldown);

			CanAttack = true;
		}

		public void Attack()
		{
			if (!CanAttack)
			{
				return;
			}

			SetAttacking(true, typeof(AttackMember));
			CanAttack = false;

			StartCoroutine(CooldownWait());

			_sprRenderer.enabled = true;
			_selfAnimator.Play("Base Layer.Attack Anim");

            _playerAnimator.Play("Base Layer.Attack");
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			Debug.Log("HIT");
			collision.GetComponent<IDamage>().ApplyDamage(new Vector2(10, 10), 1);
		}
	}
}