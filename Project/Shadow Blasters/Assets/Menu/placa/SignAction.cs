using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SignAction : MonoBehaviour, IButtonAction
{
	public void ButtonPress()
	{
		SceneManager.LoadScene(1);
	}
}
