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

		private GameObject _root;
		private PropertiesCore _propsCore;
		private DashMember _dashMember;

		private void Awake()
		{
            _root = GetRoot();
            _inputMember = transform.parent.GetComponent<InputMember>();
			_root.GetComponent<BaseMember>().RegisterChild("Move", this);
		}
		void Start()
		{
		
			_propsCore = _root.GetComponent<PropertiesCore>();

			_rigidbody = _root.GetComponent<Rigidbody2D>();
			_originalPlayerScale = _root.transform.localScale;

			_dashMember = _inputMember.GetChild("Dash") as DashMember;
		}

		private void Update()
		{
			if (_inputMember.MoveInput != 0f && !_propsCore.Attacking && !_dashMember.Dashing)
			{
				_root.transform.localScale = new Vector2(_inputMember.MoveInput * _originalPlayerScale.x, _originalPlayerScale.y);
			}
		}
		void FixedUpdate()
		{
			_rigidbody.velocity = new Vector2(MoveSpeed * _inputMember.MoveInput, _rigidbody.velocity.y);
		}
	}

}
