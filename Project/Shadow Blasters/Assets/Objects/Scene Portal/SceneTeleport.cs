using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleport : MonoBehaviour
{

    [SerializeField] private Scene _targetScene;
    [SerializeField] private Vector2 _targetPosition;

    void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.CompareTag("Player"))
		{
            StartCoroutine(LoadLevelCoroutine());
		}
	}

    private IEnumerator LoadLevelCoroutine()
    {
		TransitionController.s_Animator.SetTrigger("Start");

		yield return new WaitForSeconds(TransitionController.s_TransitionTime);

		SceneManager.LoadScene(_targetScene.name);
		Player.PropertiesCore.Player.transform.position = _targetPosition;
	}
}
