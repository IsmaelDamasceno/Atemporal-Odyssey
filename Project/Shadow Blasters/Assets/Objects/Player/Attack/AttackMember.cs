using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	/// <summary>
	/// Controla o Slash ao atacar
	/// </summary>
	public class AttackMember : BaseMember
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
				GetComponent<PropertiesCore>().Attacking = _attackingProp.Value;
			}
			return couldSet;
		}

		[SerializeField] private float _damage;
		[SerializeField] public float _cooldown;
		private bool _attackingLastFrame;

		private SpriteRenderer _sprRenderer;

		private Animator _selfAnimator;
		private Animator _playerAnimator;

		private InputMember _inputMember;

		void Awake()
		{
			#region Animation Setup
			_sprRenderer = GetComponent<SpriteRenderer>();
			_selfAnimator = GetComponent<Animator>();
			_playerAnimator = GetComponent<Animator>();

			CanAttack = true;
			#endregion

			#region Attack Controls
			_inputMember = GetComponent<InputMember>();
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
		}

		private IEnumerator Cooldown()
		{
			yield return new WaitForSeconds(_cooldown);

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

			StartCoroutine(Cooldown());

			_sprRenderer.enabled = true;
			_selfAnimator.Play("Base Layer.Attack Anim");
		}
	}
}