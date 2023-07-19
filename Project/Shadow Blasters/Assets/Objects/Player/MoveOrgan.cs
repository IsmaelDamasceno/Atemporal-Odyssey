using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class MoveOrgan : BaseOrgan
	{

		[SerializeField] private float _moveSpeed;

		private Vector2 _originalPlayerScale;
		private InputOrgan _inputOrgan;
		private Rigidbody2D _rigidbody;

		private GameObject _root;
		private PropertiesCore _propsCore;
		private DashOrgan _dashOrgan;

		private void Awake()
		{
			_inputOrgan = transform.parent.GetComponent<InputOrgan>();
			_inputOrgan.RegisterChild("Move", this);
		}
		void Start()
		{
			_root = GetRoot();
			_propsCore = _root.GetComponent<PropertiesCore>();

			_rigidbody = _root.GetComponent<Rigidbody2D>();
			_originalPlayerScale = _root.transform.localScale;

			_dashOrgan = _inputOrgan.GetChild("Dash") as DashOrgan;
		}

		private void Update()
		{
			if (_inputOrgan.MoveInput != 0f && !_propsCore.Attacking && !_dashOrgan.Dashing)
			{
				_root.transform.localScale = new Vector2(_inputOrgan.MoveInput * _originalPlayerScale.x, _originalPlayerScale.y);
			}
		}
		void FixedUpdate()
		{
			_rigidbody.velocity = new Vector2(_moveSpeed * _inputOrgan.MoveInput, _rigidbody.velocity.y);
		}
	}

}
