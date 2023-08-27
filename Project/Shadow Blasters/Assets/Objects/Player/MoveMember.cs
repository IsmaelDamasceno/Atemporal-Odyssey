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

		private void Awake()
		{
			_inputMember = GetComponent<InputMember>();
			_rigidbody = GetComponent<Rigidbody2D>();
			_sprRenderer = GetComponent<SpriteRenderer>();
		}

		private void Update()
		{
			if (_inputMember.MoveInput != 0f)
			{
				_sprRenderer.flipX = (_inputMember.MoveInput < 0f) ? true : false;
			}
		}

		void FixedUpdate()
		{
			_rigidbody.velocity = new Vector2(MoveSpeed * _inputMember.MoveInput, _rigidbody.velocity.y);
		}
	}
}
