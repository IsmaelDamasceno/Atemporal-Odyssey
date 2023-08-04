using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Classe representando um Item que reside em um Slot no Inventory System
/// </summary>
public class SlotItem : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    public bool Held;
    public Image Image { get; private set; }
    private bool _hovered;
    [SerializeField] private Item _item;

    void Awake()
    {
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

    /// <summary>
    /// Inicializa o GameObject conforme um item
    /// </summary>
    /// <param name="newItem">Item para usar na utilização</param>
    public void InitializeItem(Item newItem)
    {
		_item = newItem;
        Image.sprite = _item.Sprite;
    }

    /// <summary>
    /// Faz com que o mouse pegue o Item
    /// </summary>
    /// <param name="eventData"></param>
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
