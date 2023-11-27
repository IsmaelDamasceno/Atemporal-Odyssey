using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsistentCanvas : MonoBehaviour
{
    public static ConsistentCanvas s_instance;

	public static bool onSoundMenu = false;
	public static bool paused = false;
	private static GameObject pauseMenu;

	void Start()
	{
		pauseMenu = GameObject.FindGameObjectWithTag("Pause Menu");
		pauseMenu.SetActive(false);
	}

	private void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (paused)
			{
				if (onSoundMenu)
				{
					PauseMenu.soundMenu.SetActive(false);
					PauseMenu.pauseMenu.SetActive(true);
					onSoundMenu = false;
				}
				else
				{
					pauseMenu.SetActive(false);
					paused = false;
					Time.timeScale = 1f;
				}
			}
			else
			{
				pauseMenu.SetActive(true);
				paused = true;
				Time.timeScale = 0f;
			}
		}
	}
}
