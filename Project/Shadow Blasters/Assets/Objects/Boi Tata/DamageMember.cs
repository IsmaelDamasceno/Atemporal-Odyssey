using CrystalBot;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                    GameObject obj = Instantiate(piecesPrefab, transform.position, Quaternion.identity);
                    obj.GetComponent<DeathAudioPlayer>().PlayDeath();
                    obj.GetComponent<DeathAudioPlayer>().StartCoroutine(FaseFinal());

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

        IEnumerator FaseFinal()
        {
            yield return new WaitForSeconds(10f);
			TransitionController.s_Animator.SetTrigger("Start");
            Globals.InitiateControls().Disable();

			yield return new WaitForSeconds(TransitionController.s_TransitionTime);

			Globals.InitiateControls().Enable();
			SceneManager.LoadScene(3);
			Player.PropertiesCore.Player.transform.position = new Vector2(-268f, -19f);
			GameController.savePos = new Vector2(-268f, -19f);
		}
	}

}