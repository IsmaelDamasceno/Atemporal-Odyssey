using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    public bool Held;
    public Image Image { get; private set; }
    private bool _hovered;
    [SerializeField] private Item _item;

    void Awake()
    {
        Debug.Log("Awake");
        Image = GetComponent<Image>();
        transform.parent.GetComponent<Slot>().Item = this;

        if (_item != null)
        {
            InitializeItem(_item);
        }
    }

    void Update()
    {
		if (Held)
		{
			transform.position = Input.mousePosition;
		}
        if (_hovered)
        {
            if (transform.parent.TryGetComponent(out Slot slot))
            {
                slot.Image.color = slot.HoveredColor;
            }
        }
	}

    public void InitializeItem(Item newItem)
    {
        Debug.Log("Initialize Item");
		_item = newItem;
        Image.sprite = _item.Sprite;
    }


	public void OnPointerDown(PointerEventData eventData)
    {
		if (!Held)
		{
			InventoryMouse.PickItem(this);
		}
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hovered = false;
    }
}
