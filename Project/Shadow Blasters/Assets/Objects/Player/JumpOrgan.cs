using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class JumpOrgan : BaseOrgan
	{
		[SerializeField] private float _jumpStrenght;
		[SerializeField] private LayerMask _groundMask;

		private BoxCollider2D _feetCollider;
		private InputOrgan _inputOrgan;
		private Rigidbody2D _rb;

		private float _initialY;

		private void Awake()
		{
			_inputOrgan = transform.parent.GetComponent<InputOrgan>();
			_inputOrgan.RegisterChild("Jump", this);
		}
		void Start()
		{
			_rb = GetRoot().GetComponent<Rigidbody2D>();
			_feetCollider = GetRoot().GetComponent<PropertiesCore>().FeetCollider;
		}

		void Update()
		{
			if (_inputOrgan.JumpingInput)
			{
				if (OnFloor() && _rb.velocity.y <= .03f)
				{
					_rb.velocity = new Vector2(_rb.velocity.x, _jumpStrenght);
					_initialY = transform.position.y;
				}
			}
			else
			{
				float yDiff = transform.position.y - _initialY;
				if (!OnFloor() && yDiff >= 1.4f && _rb.velocity.y >= 0f)
				{
					_rb.velocity = new Vector2(_rb.velocity.x, Mathf.Abs(_rb.velocity.y * 0.4f));
				}
			}
		}

		private bool OnFloor()
		{
			return Physics2D.BoxCast(_feetCollider.transform.position, _feetCollider.size, 0, Vector2.down, 0.05f, _groundMask);
		}
	}
}