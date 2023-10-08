using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoletriggerPos : MonoBehaviour
{

    public static Vector3 savePos;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Player.PropertiesCore.Player.GetComponent<JumpMember>().OnFloor())
            {
                savePos = Player.PropertiesCore.Player.transform.position;
            }
        }
    }
}
