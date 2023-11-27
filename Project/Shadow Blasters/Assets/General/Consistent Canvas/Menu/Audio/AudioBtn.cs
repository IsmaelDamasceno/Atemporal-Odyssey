using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBtn : MonoBehaviour, IButtonAction
{
    public void ButtonPress()
    {
        ConsistentCanvas.onSoundMenu = true;
		PauseMenu.soundMenu.SetActive(true);
		PauseMenu.pauseMenu.SetActive(false);
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
