using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class AttackOrgan : BaseOrgan
	{
		/// <summary>
		/// Tells if the player is able to attack
		/// </summary>
		public bool CanAttack { get; set; }

		/// <summary>
		/// Create a Restrict Property To tell if the player is attacking or not, wich can be modified only by the FinishAttack class
		/// </summary>
		private RestrictProp<bool> _attackingProp = new RestrictProp<bool>(false, typeof(AttackOrgan), typeof(FinishAttack));
		/// <returns>Wheter or not the player is attacking</returns>
		public bool GetAttacking()
		{
			return _attackingProp.Value;
		}

		/// <summary>
		/// Tries to set the value of attacking, raises exception if accessed by unauthroized class
		/// </summary>
		/// <param name="value">The value to set attacking to</param>
		/// <param name="classType">The class type for verification</param>
		/// <returns></returns>
		/// <exception cref="UnityException"></exception>
		public bool SetAttacking(bool value, Type classType)
		{
			bool couldSet = _attackingProp.TrySet(value, classType);
			if (couldSet)
			{
				_root.GetComponent<PropertiesCore>().Attacking = _attackingProp.Value;
			}
			return couldSet;
		}

		/// <summary>
		/// Damage caused by attack
		/// </summary>
		[SerializeField] private float _damage;

		/// <summary>
		/// The time it takes before the player is able to attack again
		/// </summary>
		[SerializeField] private float _cooldown;

		/// <summary>
		/// Tells the state of attacking in the last frame
		/// </summary>
		private bool _attackingLastFrame;

		/// <summary>
		/// Attack Game Object's Sprite Renderer
		/// </summary>
		private SpriteRenderer _sprRenderer;

		/// <summary>
		/// Attack Game Object's Animator
		/// </summary>
		private Animator _selfAnimator;

		/// <summary>
		/// Player Game Object's Animator
		/// </summary>
		private Animator _playerAnimator;

		/// <summary>
		/// Input Organ
		/// </summary>
		private InputOrgan _inputOrgan;

		/// <summary>
		/// Root entity
		/// </summary>
		private GameObject _root;

		void Awake()
		{
			_root = GetRoot();

			#region Animation Setup
			_sprRenderer = GetComponent<SpriteRenderer>();
			_selfAnimator = GetComponent<Animator>();
			_playerAnimator = _root.GetComponent<Animator>();

			CanAttack = true;
			#endregion

			#region Attack Controls
			_inputOrgan = transform.parent.GetComponent<InputOrgan>();
			#endregion
		}

		private void Update()
		{
			// One frame activation for the Attack method
			bool attacking = _inputOrgan.AttackInput;
			if (attacking && !_attackingLastFrame)
			{
				Attack();
			}
			_attackingLastFrame = attacking;
		}

		/// <summary>
		/// Attack Cooldown
		/// </summary>
		/// <returns>Amount of seconds to wait</returns>
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

			SetAttacking(true, typeof(AttackOrgan));
			CanAttack = false;

			StartCoroutine(Cooldown());

			_sprRenderer.enabled = true;
			_selfAnimator.Play("Base Layer.Attack Anim");
		}
	}
}