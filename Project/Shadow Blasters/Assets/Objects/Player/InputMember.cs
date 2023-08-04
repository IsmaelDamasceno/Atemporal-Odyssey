using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	/// <summary>
	/// Controla e seta os inputs de teclado
	/// </summary>
	public class InputMember : BaseMember
	{
		private PlayerControls _controls;

		[HideInInspector] public float MoveInput;
		[HideInInspector] public bool JumpingInput;
		[HideInInspector] public bool DashingInput;
		[HideInInspector] public bool InteractInput;
		[HideInInspector] public bool AttackInput;
		[HideInInspector] public float Direction;

		void Awake()
		{
			Direction = 1f;
			transform.parent.GetComponent<PropertiesCore>().RegisterChild("Input", this);

			#region Controls
			_controls = Globals.InitiateControls();

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
			#region Interact Input
			_controls.Player.Interact.performed += (_) => { InteractInput = true; };
			_controls.Player.Interact.canceled += (_) => { InteractInput = false; };
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
				if (InventoryManager.Open)
				{
					InventoryAnimations.CloseInventory();
				}
				else
				{
					InventoryAnimations.OpenInventory();
				}
			}
		}
	}
}
