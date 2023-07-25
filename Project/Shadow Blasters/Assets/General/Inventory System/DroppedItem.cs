using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{

    [SerializeField] private GameObject _slotItem;

    private Player.InputOrgan _playerInputs;

    void Start()
    {
        _playerInputs =
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player.PropertiesCore>().GetChild("Input") as Player.InputOrgan;
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _playerInputs.InteractInput)
        {
            Destroy(gameObject);
        }
    }

}
