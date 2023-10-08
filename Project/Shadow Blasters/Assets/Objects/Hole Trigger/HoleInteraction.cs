using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RestartCoroutine()
    {
		TransitionController.s_Animator.SetTrigger("Start");
		Player.PropertiesCore.Player.SetActive(false);

		yield return new WaitForSeconds(TransitionController.s_TransitionTime);

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Player.PropertiesCore.Player.SetActive(true);
        HealthSystem.ChangeHealth(-1);
		Player.PropertiesCore.Player.transform.position = HoletriggerPos.savePos;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(RestartCoroutine());
		}
    }
}
