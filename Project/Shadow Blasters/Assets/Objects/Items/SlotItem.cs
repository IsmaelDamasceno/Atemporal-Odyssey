using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour, IPointerDownHandler
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
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"Mouse down on {eventData.pointerCurrentRaycast.gameObject.name}");
		if (!Held)
		{
			InventoryMouse.PickItem(this);
		}
	}
}
