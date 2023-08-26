using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
	/// <summary>
	/// Controla o sistema de pulo do jogador
	/// </summary>
	public class JumpMember : BaseMember
	{
		[SerializeField] private float _jumpStrenght;
		[SerializeField] private LayerMask _groundMask;

		private BoxCollider2D _feetCollider;
		private InputMember _inputMember;
		private Rigidbody2D _rb;
		public bool JumpControl = true;

		private float _initialY;

		private bool _startedJump = false;

		private void Awake()
		{
			_inputMember = GetComponent<InputMember>();
		}
		void Start()
		{
			_rb = GetComponent<Rigidbody2D>();
			_feetCollider = GetComponent<PropertiesCore>().FeetCollider;
		}

		void Update()
		{
			if (!JumpControl)
			{
				return;
			}

			if (_inputMember.JumpingInput)
			{
				if (OnFloor() && _rb.velocity.y <= .03f)
				{
					_rb.velocity = new Vector2(_rb.velocity.x, _jumpStrenght);
					_initialY = transform.position.y;
					_startedJump = true;

					Debug.Log("Setting Y by direct jump");
				}
			}
			else
			{
				bool grounded = OnFloor();
				float yDiff = transform.position.y - _initialY;
				if (!grounded && yDiff >= 1.4f && _rb.velocity.y >= 0f && _startedJump)
				{
					Debug.Log("Setting Y by variable jump");
					_rb.velocity = new Vector2(_rb.velocity.x, Mathf.Abs(_rb.velocity.y * 0.4f));
				}
				else if (grounded)
				{
					_startedJump = false;
				}
			}
		}

		public bool OnFloor()
		{
			return Physics2D.BoxCast(_feetCollider.transform.position, _feetCollider.size, 0, Vector2.down, 0.05f, _groundMask);
		}
	}
}
