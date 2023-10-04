using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashAction : MonoBehaviour, IButtonAction
{
    public void ButtonPress()
    {
        Application.Quit();
    }
}
