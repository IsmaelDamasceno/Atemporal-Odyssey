using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPopUpScript : MonoBehaviour
{
    void Start()
    {
        Player.InteractionMember.interactObject = gameObject;
    }

    void Update()
    {
        
    }
}
