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
		public bool Attacking = false;

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

		[SerializeField] private int _damage;
		[SerializeField] private float _impact;
		public float Cooldown;
		private bool _attackingLastFrame;

		private SpriteRenderer _sprRenderer;
		public BoxCollider2D attackCollider;

		private Animator _selfAnimator;
		private Animator _playerAnimator;

		private InputMember _inputMember;
		private float _scale;
		void Awake()
		{
			transform.parent.GetComponent<PropertiesCore>().RegisterMember("Attack", this);
			_scale = transform.localScale.x;
			attackCollider = GetComponent<BoxCollider2D>();
			attackCollider.enabled = false;

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
				transform.localScale = new Vector2(_scale * _inputMember.MoveInput, transform.localScale.y);
			}
		}

		public IEnumerator CooldownWait()
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

			PropertiesCore.audioPlayer.PlayAttack();
			SetAttacking(true, typeof(AttackMember));
			CanAttack = false;
			Attacking = true;
			attackCollider.enabled = true;

			_sprRenderer.enabled = true;
			_selfAnimator.Play("Base Layer.Attack Anim");

			_playerAnimator.SetBool("Attacking", true);
            _playerAnimator.Play("Base Layer.Attack");
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (Attacking)
			{
				IDamage attackedTarget = collision.GetComponent<IDamage>();
				
				if (attackedTarget != null)
				{
                    attackedTarget.ApplyDamage(transform, new Vector2(_impact * transform.localScale.x, _impact), _damage);
                }
			}
		}
	}
}