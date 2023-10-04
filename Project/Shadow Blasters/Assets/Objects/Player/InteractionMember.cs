using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
	/// <summary>
	/// Controla as interações do jogador com outros objetos (Items largados, Chests, etc)
	/// </summary>
	public class InteractionMember : BaseMember
	{
		[SerializeField] private float _interactionDistance;
		[SerializeField] private LayerMask _interactionMask;
		[SerializeField] private GameObject waterSplashParticles;

		private InputMember _inputMember;

		private bool _interactingLastFrame = false;
		private GameObject interactObject;

		void Awake()
		{
			_inputMember = GetComponent<InputMember>();
			SceneManager.sceneLoaded += OnSceneLoad;
		}
		void OnSceneLoad(Scene scene, LoadSceneMode mode)
		{
			interactObject = GameObject.FindGameObjectWithTag("Interact");
		}

		void Update()
		{
			if (PropertiesCore.canInteract)
			{
				Interact();
			}
		}

		/// <summary>
		/// Executa uma interação
		/// </summary>
		void Interact()
		{
			// Procura por objetos interagiveis em um raio definido por _interactionDistance
			RaycastHit2D[] rayHitList = Physics2D.CircleCastAll(transform.position, _interactionDistance, Vector2.up, 0f, _interactionMask);

			// Caso algum objeto tenha sido encontrado
			if (rayHitList.Length > 0)
			{
				if (_inputMember.InteractInput && !_interactingLastFrame)
				{
					// Passar por cada Objeto no raio para executar interação
					foreach (RaycastHit2D raycastHit in rayHitList)
					{
						IInteractable interactable = raycastHit.collider.GetComponent<IInteractable>();
						// Executa a interação, e cancela o loop caso essa tenha sido em sucedida
						if (interactable.Interact())
						{
							break;
						}
					}
				}
				_interactingLastFrame = _inputMember.InteractInput;

				interactObject.SetActive(true);
			}
			else
			{
				interactObject.SetActive(false);
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
			{
				Vector2 pos = PropertiesCore.s_Instance.FeetCollider.transform.position;
				pos.y -= 0.2f;
				Instantiate(waterSplashParticles, pos, Quaternion.identity);
			}
		}
		private void OnTriggerExit2D(Collider2D collision)
		{
			if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
			{
				Vector2 pos = PropertiesCore.s_Instance.FeetCollider.transform.position;
				pos.y -= 0.2f;
				Instantiate(waterSplashParticles, pos, Quaternion.identity);
			}
		}
	}
}