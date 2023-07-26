using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [SerializeField] private Item _item;
    private SpriteRenderer _image;

    private Player.InputOrgan _playerInputs;

    private void Awake()
    {
        _image = GetComponent<SpriteRenderer>();
        if (_item != null)
        {
            InitializeItem(_item);
        }
    }

    void Start()
    {
        _playerInputs =
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player.PropertiesCore>().GetChild("Input") as Player.InputOrgan;
	}

    public void InitializeItem(Item newItem)
    {
        _item = newItem;
        _image.sprite = _item.Sprite;
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _playerInputs.InteractInput)
        {
			InventoryManager.CreateInvItem(_item);
            Destroy(gameObject);
		}
    }
}
