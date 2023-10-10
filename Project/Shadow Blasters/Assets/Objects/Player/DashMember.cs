using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
	/// <summary>
	/// Controla o sistema de Dash do jogador
	/// </summary>
	public class DashMember : MonoBehaviour
	{
		[HideInInspector] public bool Dashing;

		[SerializeField] private float _dashSpeed;
		[SerializeField] private float _dashTime;
		[SerializeField] private float _dashCooldown;

		private InputMember _inputMember;
		private bool _ableToDash = true;
		private bool _dashingLastFrame = false;
		private Rigidbody2D _rb;
		private float _dashDirection;
		private float _originalGravScale;
		private Animator animator;
        private PropertiesCore propertiesCore;

        private IEnumerator PerformDash()
		{
            PropertiesCore.audioPlayer.PlayDash();
            yield return new WaitForSeconds(_dashTime);

            propertiesCore.ChangeState(PlayerState.Free);
            Dashing = false;
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
			_inputMember = GetComponent<InputMember>();
			propertiesCore = GetComponent<PropertiesCore>();
		}
		void Start()
		{
			animator = GetComponent<Animator>();

			_rb = GetComponent<Rigidbody2D>();
			_originalGravScale = _rb.gravityScale;

			SceneManager.sceneLoaded += OnLoad;
		}
		public void OnLoad(Scene scene, LoadSceneMode mode)
		{
			StopAllCoroutines();
			_ableToDash = true;
			Dashing = false;
		}

		void Update()
		{
			bool dashInput = _inputMember.DashingInput;
			if (dashInput && !_dashingLastFrame && _ableToDash)
			{
				propertiesCore.ChangeState(PlayerState.Dash);
				Dashing = true;
				_rb.gravityScale = 0f;
				_dashDirection = _inputMember.Direction;
				_ableToDash = false;
				StartCoroutine(PerformDash());
			}
			_dashingLastFrame = dashInput;

			animator.SetBool("Dashing", Dashing);
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
