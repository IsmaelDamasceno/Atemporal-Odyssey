using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public bool Open { get; private set; }
    private Player.InputOrgan _playerInputs;
    private Animator _animator;

    void Start()
    {
		_animator = GetComponent<Animator>();
        _playerInputs =
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player.PropertiesCore>().GetChild("Input") as Player.InputOrgan;
    }

    void Update()
    {
        
    }

    public void SetChest(bool value)
    {
        Open = value;
        _animator.SetBool("Open", Open);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _playerInputs.InteractInput)
        {
            SetChest(true);
        }
    }
}
