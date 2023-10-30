using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Player
{
	/// <summary>
	/// Controla e seta os inputs de teclado
	/// </summary>
	public class InputMember : BaseMember
	{
		private PlayerControls _controls;

		[HideInInspector] public float MoveInput;
		[HideInInspector] public float LadderInput;
		[HideInInspector] public bool JumpingInput;
		[HideInInspector] public bool DashingInput;
		[HideInInspector] public bool InteractInput;
		[HideInInspector] public bool AttackInput;
		[HideInInspector] public float Direction;

		void Awake()
		{
			Direction = 1f;

			#region Controls
			_controls = Globals.InitiateControls();

            #region Move Input
            _controls.Player.Movement.performed += (ctx) => { MoveInput = Math.Sign(ctx.ReadValue<float>()); };
			_controls.Player.Movement.canceled += (_) => { MoveInput = 0f; };
			#endregion
			#region Jump Input
			_controls.Player.Jump.performed += (_) => { JumpingInput = true; };
			_controls.Player.Jump.canceled += (_) => { JumpingInput = false; };
			#endregion
			#region Dialogue Input
			_controls.Player.Jump.performed += DialogueSystem.SkipDialogue;
            #endregion
            #region Attack Input
            _controls.Player.Attack.performed += (_) => { AttackInput = true; };
			_controls.Player.Attack.canceled += (_) => { AttackInput = false; };
			#endregion
			#region Save Pos Input
			_controls.Player.SavePos.performed += GetComponent<SavePosMember>().Save;
			#endregion
			#region Load Pos Input
			_controls.Player.LoadPos.performed += GetComponent<SavePosMember>().Load;
			#endregion
			#region Dash Input
			_controls.Player.Dash.performed += (_) => { DashingInput = true; };
			_controls.Player.Dash.canceled += (_) => { DashingInput = false; };
			#endregion
			#region Interact Input
			_controls.Player.Interact.performed += (_) => { InteractInput = true; };
			_controls.Player.Interact.canceled += (_) => { InteractInput = false; };
			#endregion
			#region Ladder Input
			_controls.Player.Ladder.performed += (ctx) => { LadderInput = ctx.ReadValue<float>(); };
			_controls.Player.Ladder.canceled += (_) => { LadderInput = 0f; };
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

			if (Input.GetKeyDown(KeyCode.Tab))
			{
				BUffUIManager.SetBuffUI();
			}
		}
	}
}
