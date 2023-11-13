using Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController instance;
    public static int Score;

    public static TextMeshProUGUI coinAmount;
    public static Vector2 savePos;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += LoadScene;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void GetCoin(int amount, GameObject coin)
    {
        Score += amount;
        Destroy(coin);
        coinAmount.text = Score.ToString();
    }

    private void LoadScene(Scene scene, LoadSceneMode mode)
    {
        coinAmount = GameObject.FindGameObjectWithTag("Coin Amount").GetComponent<TextMeshProUGUI>();

        if (scene.buildIndex == 1)
        {
            if (Player.PropertiesCore.Player.GetComponent<DashMember>().enabled)
            {
                Destroy(GameObject.FindGameObjectWithTag("DashPowerUp"));
            }
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public static void RestartPlayer()
    {
        Player.PropertiesCore.Player.transform.position = savePos;
    }

    public static void SavePlayerInitialPos(GameObject playerObj)
    {
        savePos = playerObj.transform.position;
    }
}
