using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    public static Vector2 savePos;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
