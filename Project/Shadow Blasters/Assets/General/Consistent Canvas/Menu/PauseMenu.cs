using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public static GameObject pauseMenu;
	public static GameObject soundMenu;

    void Start()
    {
        pauseMenu = transform.GetChild(1).gameObject;
        soundMenu = transform.GetChild(2).gameObject;
    }

    void Update()
    {
        
    }
}
