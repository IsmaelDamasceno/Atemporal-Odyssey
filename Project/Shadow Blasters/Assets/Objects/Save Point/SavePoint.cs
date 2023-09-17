
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    public bool Interact()
    {
        GameController.savePos = Player.PropertiesCore.Player.transform.position;
        return true;
    }
}
