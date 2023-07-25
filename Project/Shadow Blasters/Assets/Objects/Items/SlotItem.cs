using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private GameObject _heldItem;

    public bool Held;
    public Image Image { get; private set; }
    private bool _hovered;

    void Start()
    {
        Image = GetComponent<Image>();
        transform.parent.GetComponent<Slot>().Item = this;
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
