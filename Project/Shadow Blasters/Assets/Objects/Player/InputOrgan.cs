using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class InputOrgan : BaseOrgan
	{
		private PlayerControls _controls;

		[HideInInspector] public float MoveInput;
		[HideInInspector] public bool JumpingInput;
		[HideInInspector] public bool DashingInput;
		[HideInInspector] public bool AttackInput;
		[HideInInspector] public float Direction;

		void Awake()
		{
			_controls = Globals.InitiateControls();
			Direction = 1f;

			#region Controls
			#region Move Input
			_controls.Player.Movement.performed += (ctx) => { MoveInput = ctx.ReadValue<float>(); };
			_controls.Player.Movement.canceled += (_) => { MoveInput = 0f; };
			#endregion
			#region Jump Input
			_controls.Player.Jump.performed += (_) => { JumpingInput = true; };
			_controls.Player.Jump.canceled += (_) => { JumpingInput = false; };
			#endregion
			#region Attack Input
			_controls.Player.Attack.performed += (_) => { AttackInput = true; };
			_controls.Player.Attack.canceled += (_) => { AttackInput = false; };
			#endregion
			#region Dash Input
			_controls.Player.Dash.performed += (_) => { DashingInput = true; };
			_controls.Player.Dash.canceled += (_) => { DashingInput = false; };
			#endregion
			#endregion
		}

		private void OnEnable() => _controls.Enable();
		private void OnDisable() => _controls.Disable();

		void Update()
		{
			if (MoveInput != 0f)
			{
				Direction = MoveInput;
			}
		}
	}
}
