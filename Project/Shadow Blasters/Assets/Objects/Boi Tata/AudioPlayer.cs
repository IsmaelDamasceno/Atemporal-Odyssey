using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BoiTata {
	public class AudioPlayer : MonoBehaviour
	{
		[Header("Fireball")]
		[SerializeField] private AudioClip fireballClip;
		[SerializeField] private List<AudioClip> fireballTouchGround;

		[Header("Swing")]
		[SerializeField] private AudioClip swingSideClip;
		[SerializeField] private AudioClip swingUpClip;
		[SerializeField] private AudioClip swingUpPrepareClip;

		private AudioSource source;

		private void Start()
		{
			source = GetComponent<AudioSource>();
		}

		public void PlayFireball()
		{
			source.PlayOneShot(fireballClip);
		}
		public void PlayFireballTouchGround()
		{
			int index = Random.Range(0, fireballTouchGround.Count);
			source.PlayOneShot(fireballTouchGround[index]);
		}
		public void PlaySwingSide()
		{
			source.PlayOneShot(swingSideClip);
		}
		public void PlaySwingUp()
		{
			source.PlayOneShot(swingUpClip);
		}
		public void PlaySwingUpPrepare()
		{
			source.PlayOneShot(swingUpPrepareClip);
		}
	}
}

