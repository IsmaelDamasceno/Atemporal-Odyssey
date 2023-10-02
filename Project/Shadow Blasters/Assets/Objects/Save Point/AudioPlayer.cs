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
            source.PlayOneShot(burstClip);
        }
		public void PlaySaveEndSound()
		{
			source.PlayOneShot(saveClip);
		}
	}
}

