using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class ContinuarBtn : MonoBehaviour, IButtonAction
{
    public void ButtonPress()
    {
        GameObject.FindGameObjectWithTag("Pause Menu").SetActive(false);
        ConsistentCanvas.paused = false;
		Time.timeScale = 1f;
	}

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
