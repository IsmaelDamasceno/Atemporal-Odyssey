using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class InteractionMember : BaseMember
	{
		[SerializeField] private float _interactionDistance;
		[SerializeField] private LayerMask _interactionMask;
		private InputMember _inputMember;

		private bool _interactingLastFrame = false;

		void Awake()
		{
			_inputMember = transform.parent.GetComponent<InputMember>();
			_inputMember.RegisterChild("Interaction", this);
		}

		void Update()
		{
			if (_inputMember.InteractInput && !_interactingLastFrame)
			{
				Interact();
			}
			_interactingLastFrame = _inputMember.InteractInput;
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