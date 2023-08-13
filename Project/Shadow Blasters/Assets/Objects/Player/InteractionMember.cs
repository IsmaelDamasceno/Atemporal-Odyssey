using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	/// <summary>
	/// Controla as intera��es do jogador com outros objetos (Items largados, Chests, etc)
	/// </summary>
	public class InteractionMember : BaseMember
	{
		[SerializeField] private float _interactionDistance;
		[SerializeField] private LayerMask _interactionMask;
		private InputMember _inputMember;

		private bool _interactingLastFrame = false;

		void Awake()
		{
			_inputMember = GetComponent<InputMember>();
		}

		void Update()
		{
			// Executa ao pressionar o bot�o de intera��o
			if (_inputMember.InteractInput && !_interactingLastFrame)
			{
				Interact();
			}
			_interactingLastFrame = _inputMember.InteractInput;
		}

		/// <summary>
		/// Executa uma intera��o
		/// </summary>
		void Interact()
		{
			// Procura por objetos interagiveis em um raio definido por _interactionDistance
			RaycastHit2D[] rayHitList = Physics2D.CircleCastAll(transform.position, _interactionDistance, Vector2.up, 0f, _interactionMask);

			// Caso algum objeto tenha sido encontrado
			if (rayHitList.Length > 0)
			{
				// Passar por cada Objeto no raio para executar intera��o
				foreach (RaycastHit2D raycastHit in rayHitList)
				{
					IInteractable interactable = raycastHit.collider.GetComponent<IInteractable>();
					// Executa a intera��o, e cancela o loop caso essa tenha sido em sucedida
					if (interactable.Interact())
					{
						break;
					}
				}
			}
		}
	}
}