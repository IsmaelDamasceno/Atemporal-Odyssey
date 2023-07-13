using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpStrenght;
    [SerializeField] private LayerMask _groundMask;

    private Rigidbody2D _rb;
    private PlayerControls _controls;
    private float _movement;
    private Vector3 _originalScale;
    private BoxCollider2D _feetCollider;

    public PlayerControls GetControls()
    {
        return _controls;
    }

    void Awake()
    {
        _feetCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        _originalScale = transform.localScale;

        _rb = GetComponent<Rigidbody2D>();

		_controls = Globals.InitiateControls();

		#region Movement Controls
		// Put the value of "Movement" input in _movement value when "Movement" is performed
		_controls.Player.Movement.performed += (ctx) => { _movement = ctx.ReadValue<float>(); };

		// Change the value of _movement to 0 when the Movement action is canceled (no button presses)
		_controls.Player.Movement.canceled += (_) => { _movement = 0f; };
        #endregion

        #region Jump Controls
        _controls.Player.Jump.started += (_) => { Jump(); };
		#endregion
	}

	private void OnEnable() => _controls.Enable();
    private void OnDisable() => _controls.Disable();

    void Update()
    {
        OnFloor();
        if (_movement != 0f)
        {
            transform.localScale = new Vector3(_originalScale.x * _movement, _originalScale.y, _originalScale.z);
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_movement * _moveSpeed, _rb.velocity.y);
    }

    private bool Jump()
    {
        bool grounded = OnFloor();
        if (grounded)
        {
			_rb.velocity = new Vector2(_rb.velocity.x, _jumpStrenght);
		}
        return grounded;
	}
    private bool OnFloor()
    {
		return Physics2D.BoxCast(_feetCollider.transform.position, _feetCollider.size, 0, Vector2.down, 0.1f, _groundMask);
	}
}
