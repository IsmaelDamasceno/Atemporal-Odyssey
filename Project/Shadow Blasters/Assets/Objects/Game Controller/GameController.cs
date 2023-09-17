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

            savePos = Player.PropertiesCore.Player.transform.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }

    public static void RestartPlayer()
    {
        Player.PropertiesCore.Player.transform.position = savePos;
    }
}
