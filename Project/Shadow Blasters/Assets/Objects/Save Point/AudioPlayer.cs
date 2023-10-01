using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SavePoint {
	public class AudioPlayer : MonoBehaviour
	{
		[SerializeField] private AudioClip burstClip;
		[SerializeField] private AudioClip saveClip;

		private AudioSource source;

		void Start()
		{
			source = GetComponent<AudioSource>();
		}

		void Update()
		{

		}

		public void PlaySaveSound()
		{
			StartCoroutine(PlayBurst());
		}
		public void PlaySaveEndSound()
		{
			source.PlayOneShot(saveClip);
		}

		IEnumerator PlayBurst()
		{
			for(int i = 0; i < 3; i++)
			{
				yield return new WaitForSeconds(0.5f);
				source.PlayOneShot(burstClip);
			}
		}
	}
}

