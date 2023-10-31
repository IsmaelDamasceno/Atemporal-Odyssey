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
		private Animator _animator;
		public bool JumpControl = true;
		public static bool grounded = false;

		private float _initialY;

		public bool _startedJump = false;

		private void Awake()
		{
			_inputMember = GetComponent<InputMember>();
			_animator = GetComponent<Animator>();
		}
		void Start()
		{
			_rb = GetComponent<Rigidbody2D>();
			_feetCollider = GetComponent<PropertiesCore>().FeetCollider;
		}

		void Update()
		{
			grounded = OnFloor();
			if (JumpControl)
			{
				if (_inputMember.JumpingInput)
				{
					if (grounded || PropertiesCore.CanJump())
					{
						PropertiesCore.ExitLadder();
						_rb.velocity = new Vector2(_rb.velocity.x, _jumpStrenght);
						_initialY = transform.position.y;
						_startedJump = true;
					}
				}
				else
				{
				
					float yDiff = transform.position.y - _initialY;
					if (!grounded && yDiff >= 1.4f && _rb.velocity.y >= 0f && _startedJump)
					{
						_rb.velocity = new Vector2(_rb.velocity.x, Mathf.Abs(_rb.velocity.y * 0.4f));
					}
					else if (grounded)
					{
						_startedJump = false;
					}
				}
			}
			_animator.SetFloat("YSpeed", _rb.velocity.y);
			_animator.SetBool("Grounded", grounded);
		}

		public bool OnFloor()
		{
			return Physics2D.BoxCast(_feetCollider.transform.position, _feetCollider.size, 0, Vector2.down, 0.05f, _groundMask);
		}
	}
}
