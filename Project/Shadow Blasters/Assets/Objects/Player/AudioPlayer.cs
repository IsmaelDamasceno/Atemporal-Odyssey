using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class AudioPlayer : MonoBehaviour
	{
		[SerializeField] private AudioClip attackClip;
        [SerializeField] private AudioClip stepClip1;
        [SerializeField] private AudioClip stepClip2;
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

		}

		public void PlayAttack()
		{
			source.PlayOneShot(attackClip);
		}

        public void PlayStep(int step)
        {
			if (step == 1)
				source.PlayOneShot(stepClip1);
			else if (step == 2)
				source.PlayOneShot(stepClip2);
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
