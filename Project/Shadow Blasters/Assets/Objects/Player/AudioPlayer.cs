using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class AudioPlayer : MonoBehaviour
	{
		[SerializeField] private AudioClip attackClip;

		private AudioSource source;

		void Start()
		{
			source = GetComponent<AudioSource>();
		}

		void Update()
		{

		}

		public void PlayAttack()
		{
			source.PlayOneShot(attackClip);
		}
	}
}
