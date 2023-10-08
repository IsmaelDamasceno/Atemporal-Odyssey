using CrystalBot;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoiTata
{
    public class DamageMember : MonoBehaviour, IDamage
    {
        [SerializeField] private float flashTime;
        [SerializeField] private GameObject piecesPrefab;

        private bool stunned = false;
        public int health;

        IEnumerator StunCoroutine()
        {
            yield return new WaitForSeconds(flashTime);
            stunned = false;
        }

        public void ApplyDamage(Transform hitTransform, Vector2 impact, int amount)
        {
            if (!stunned)
            {
                if (health <= 0)
                {
                    Instantiate(piecesPrefab, transform.position, Quaternion.identity).GetComponent<DeathAudioPlayer>().PlayDeath();
                    Timer.playing = false;
                    Destroy(gameObject);
					return;
                }


				if (TryGetComponent<FlashWhite>(out var component))
				{
					component.EndComponent();
				}
				gameObject.AddComponent<FlashWhite>().Init(flashTime, flashTime, GetComponent<SpriteRenderer>());

                StopAllCoroutines();
                StartCoroutine(StunCoroutine());
                health--;
                stunned = true;
			}
        }
    }

}