using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Gerencia ações e armazenamento de Slots individuais no Inventory System
/// </summary>
public class Slot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool _hovered;
	private bool _selected;

    public Image Image { get; private set; }
    public SlotItem Item;
    public Color DefaultColor;
    public Color SelectedColor;
    public Color HoveredColor;

    void Awake()
    {
		Image = GetComponent<Image>();
    }
    void Update()
    {
        if (_hovered && Item == null)
        {

		}
	}

	/// <summary>
	/// Ativa/desativa o Slot
	/// </summary>
	/// <param name="val">True: ativa o slot, false: desativa o slot</param>
    public void SetActive(bool val)
    {
		_selected = val;
		if (!_hovered)
		{
			Image.color = (val == true) ? SelectedColor : DefaultColor;
		}
    }

	#region Pointer Handlers
	public void OnPointerDown(PointerEventData eventData)
	{
		if (InventoryMouse.HeldItem != null)
		{
			if (Item == null)
			{
				InventoryMouse.PlaceItem(this);
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		_hovered = true;
		Image.color = HoveredColor;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_hovered = false;
		Image.color = (_selected == true) ? SelectedColor : DefaultColor;
	}
	#endregion
}
