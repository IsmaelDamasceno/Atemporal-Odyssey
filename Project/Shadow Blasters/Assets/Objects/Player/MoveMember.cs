using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class MoveMember : BaseMember
	{

		[SerializeField] private float _moveSpeed;

		private Vector2 _originalPlayerScale;
		private InputMember _inputMember;
		private Rigidbody2D _rigidbody;

		private GameObject _root;
		private PropertiesCore _propsCore;
		private DashMember _dashMember;

		private void Awake()
		{
			_inputMember = transform.parent.GetComponent<InputMember>();
			_inputMember.RegisterChild("Move", this);
		}
		void Start()
		{
			_root = GetRoot();
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
			_rigidbody.velocity = new Vector2(_moveSpeed * _inputMember.MoveInput, _rigidbody.velocity.y);
		}
	}

}
