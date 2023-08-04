using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class DashMember : BaseMember
	{
		[HideInInspector] public bool Dashing;

		[SerializeField] private float _dashSpeed;
		[SerializeField] private float _dashTime;
		[SerializeField] private float _dashCooldown;

		private InputMember _inputMember;
		private bool _ableToDash = true;
		private bool _dashingLastFrame = false;
		private MoveMember _moveMember;
		private Rigidbody2D _rb;
		private float _dashDirection;
		private float _originalGravScale;

		private IEnumerator PerformDash()
		{
			yield return new WaitForSeconds(_dashTime);
			
			Dashing = false;
			_moveMember.enabled = true;
			_rb.gravityScale = _originalGravScale;

			StartCoroutine(PerformCooldown());
		}
		private IEnumerator PerformCooldown()
		{
			yield return new WaitForSeconds(_dashCooldown);
			
			_ableToDash = true;
		}

		private void Awake()
		{
			_inputMember = transform.parent.GetComponent<InputMember>();
			_inputMember.RegisterChild("Dash", this);
		}
		void Start()
		{
			_moveMember = _inputMember.GetChild("Move") as MoveMember;

			_rb = GetRoot().GetComponent<Rigidbody2D>();
			_originalGravScale = _rb.gravityScale;
		}

		void Update()
		{
			bool dashInput = _inputMember.DashingInput;
			if (dashInput && !_dashingLastFrame && _ableToDash)
			{
				Dashing = true;
				_moveMember.enabled = false;
				_rb.gravityScale = 0f;
				_dashDirection = _inputMember.Direction;
				_ableToDash = false;
				StartCoroutine(PerformDash());
			}
			_dashingLastFrame = dashInput;
		}

		private void FixedUpdate()
		{
			if (Dashing)
			{
				_rb.velocity = new Vector2(_dashDirection * _dashSpeed, 0f);
			}
		}
	}
}
