using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleport : MonoBehaviour
{

    [SerializeField] private SceneAsset _targetScene;
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
			Debug.Log("Changing scene");
			SceneManager.LoadScene(_targetScene.name);
			Player.PropertiesCore.Player.transform.position = _targetPosition;
		}
	}
}
