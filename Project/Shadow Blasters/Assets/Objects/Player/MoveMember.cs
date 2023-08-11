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

		[SerializeField] public float MoveSpeed;

		private Vector2 _originalPlayerScale;
		private InputMember _inputMember;
		private Rigidbody2D _rigidbody;

		private PropertiesCore _propsCore;
		private DashMember _dashMember;

		private void Awake()
		{
            _inputMember = GetComponent<InputMember>();
		}
		void Start()
		{
			_propsCore = GetComponent<PropertiesCore>();
			_dashMember = GetComponent<DashMember>();

			_rigidbody = GetComponent<Rigidbody2D>();
			_originalPlayerScale = transform.localScale;
		}

		private void Update()
		{
			if (_inputMember.MoveInput != 0f && !_propsCore.Attacking && !_dashMember.Dashing)
			{
				transform.localScale = new Vector2(_inputMember.MoveInput * _originalPlayerScale.x, _originalPlayerScale.y);
			}
		}
		void FixedUpdate()
		{
			_rigidbody.velocity = new Vector2(MoveSpeed * _inputMember.MoveInput, _rigidbody.velocity.y);
		}
	}

}
