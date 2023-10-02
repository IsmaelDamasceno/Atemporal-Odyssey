
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SavePoint
{
	public class SavePointController : MonoBehaviour, IInteractable
	{
		private ParticleSystem partSystem;
		private Animator animator;
		private bool inCycle = false;
		private AudioPlayer audioPlayer;

		private Light2D light;

		private void Start()
		{
			partSystem = GetComponentInChildren<ParticleSystem>();
			partSystem.Stop();
			light = GetComponentInChildren<Light2D>();
			audioPlayer = GetComponent<AudioPlayer>();

			animator = GetComponent<Animator>();
		}

		public bool Interact()
		{
			if (inCycle)
			{
				return false;
			}
			inCycle = true;
			StartCoroutine(OpenDoor());
			return true;
		}

		IEnumerator OpenDoor()
		{
			animator.SetTrigger("Open");
			Player.PropertiesCore.canInteract = false;
			Player.PropertiesCore.Player.GetComponent<InputMember>().enabled = false;
			yield return new WaitForSeconds(0.5f);
			StartCoroutine(PlayerEnter());
		}
		IEnumerator PlayerEnter()
		{
			animator.SetTrigger("Close Light");
			light.enabled = true;

			Player.PropertiesCore.Player.SetActive(false);
			yield return new WaitForSeconds(0.25f);
			StartCoroutine(ActivateParticles());
		}

		IEnumerator ActivateParticles()
		{
			partSystem.Play();
			Transform canvasTransform = ConsistentCanvas.s_instance.transform;
			GameObject gameSavedText = Resources.Load<GameObject>("Game Saved");
			Instantiate(gameSavedText, canvasTransform);
            audioPlayer.PlaySaveSound();
            yield return new WaitForSeconds(2f);

			animator.SetTrigger("Open");
			light.enabled = false;

			Player.PropertiesCore.Player.SetActive(true);
			Player.PropertiesCore.canInteract = true;
			Player.PropertiesCore.Player.GetComponent<InputMember>().enabled = true;
			GameController.savePos = Player.PropertiesCore.Player.transform.position;
			audioPlayer.PlaySaveEndSound();
			StartCoroutine(CloseDoor());
		}
		IEnumerator CloseDoor()
		{
			yield return new WaitForSeconds(0.25f);
			animator.SetTrigger("Close");
			inCycle = false;
		}
	}
}
