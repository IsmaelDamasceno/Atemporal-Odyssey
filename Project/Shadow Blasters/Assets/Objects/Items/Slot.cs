using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerDownHandler
{
    private bool _hovered;
    private Image _image;

    public SlotItem Item;
    public Color DefaultColor;
    public Color HoverColor;

    void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
		Debug.Log($"Mouse down on {eventData.pointerCurrentRaycast.gameObject.name}");
		if (InventoryMouse.HeldItem != null)
		{
			if (Item == null)
			{
				InventoryMouse.PlaceItem(this);
			}
		}
	}

    public void SetActive(bool val)
    {
        _image.color = (val == true) ? HoverColor : DefaultColor;
    }
}
