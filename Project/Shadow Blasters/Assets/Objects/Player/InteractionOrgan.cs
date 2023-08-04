using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class InteractionOrgan : BaseOrgan
	{
		[SerializeField] private float _interactionDistance;
		[SerializeField] private LayerMask _interactionMask;
		private InputOrgan _inputOrgan;

		private bool _interactingLastFrame = false;

		void Awake()
		{
			_inputOrgan = transform.parent.GetComponent<InputOrgan>();
			_inputOrgan.RegisterChild("Interaction", this);
		}

		void Update()
		{
			if (_inputOrgan.InteractInput && !_interactingLastFrame)
			{
				Interact();
			}
			_interactingLastFrame = _inputOrgan.InteractInput;
		}

		void Interact()
		{
			RaycastHit2D[] rayHitList = Physics2D.CircleCastAll(transform.position, _interactionDistance, Vector2.up, 0f, _interactionMask);

			if (rayHitList.Length > 0)
			{
				foreach (RaycastHit2D raycastHit in rayHitList)
				{
					IInteractable interactable = raycastHit.collider.GetComponent<IInteractable>();
					if (interactable.Interact())
					{
						break;
					}
				}
			}
		}
	}
}