using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class AudioPlayer : MonoBehaviour
	{
		[SerializeField] private AudioClip attackClip;
        [SerializeField] private List<AudioClip> stepsClips;
        [SerializeField] private AudioClip dashClip;
        [SerializeField] private AudioClip waterSplashClip;
        [SerializeField] private AudioClip damageClip;
        [SerializeField] private AudioClip powerUpClip;

        private AudioSource source;

		void Start()
		{
			source = GetComponent<AudioSource>();
		}

		void Update()
		{
			source.volume = GameController.masterVolume * GameController.effectsVolume;
		}

		public void PlayAttack()
		{
			source.PlayOneShot(attackClip);
		}

        public void PlayStep()
        {
			if (!source.isPlaying)
			{
				source.clip = stepsClips[Random.Range(0, stepsClips.Count)];
				source.Play();
			}
		}

		public void PlayDash()
		{
            source.PlayOneShot(dashClip);
        }

		public void PlayWaterSplash()
		{
            source.PlayOneShot(waterSplashClip);
        }

		public void PlayDamage()
		{
            source.PlayOneShot(damageClip);
        }

		public void PlayPowerUp()
		{
            source.PlayOneShot(powerUpClip);
        }
    }
}
