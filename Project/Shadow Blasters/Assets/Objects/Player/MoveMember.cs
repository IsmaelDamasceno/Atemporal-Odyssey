using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	/// <summary>
	/// Controla o sistema de movimento do jogador
	/// </summary>
	public class MoveMember : BaseMember
	{

		public float MoveSpeed;

		private InputMember _inputMember;
		private Rigidbody2D _rigidbody;
		private SpriteRenderer _sprRenderer;

		private Animator _animator;

		private void Awake()
		{
			_inputMember = GetComponent<InputMember>();
			_rigidbody = GetComponent<Rigidbody2D>();
			_sprRenderer = GetComponent<SpriteRenderer>();
			_animator = GetComponent<Animator>();
		}

		private void Update()
		{
			if (_inputMember.MoveInput != 0f)
			{
				_sprRenderer.flipX = (_inputMember.MoveInput < 0f);
				if (JumpMember.grounded)
				{
					PropertiesCore.audioPlayer.PlayStep();
				}
			}

			_animator.SetBool("Moving", _inputMember.MoveInput != 0f);
		}

		void FixedUpdate()
		{
			_rigidbody.velocity = new Vector2(MoveSpeed * _inputMember.MoveInput, _rigidbody.velocity.y);
		}
	}
}
