using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gerencia o item segurado pelo cursor
/// </summary>
public static class InventoryMouse
{
    public static SlotItem HeldItem;

    /// <summary>
    /// Pega um Item do inventário
    /// </summary>
    /// <param name="item">Item para pegar</param>
    public static void PickItem(SlotItem item)
    {
        Slot targetSlot = item.transform.parent.GetComponent<Slot>();
        if (HeldItem != null)
        {
            PlaceItem(targetSlot);
			targetSlot.Item = HeldItem;
		}
        else
        {
			targetSlot.Item = null;
        }

		HeldItem = item;
        HeldItem.Image.raycastTarget = false;

		GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
		HeldItem.transform.SetParent(canvas.transform);
		HeldItem.Held = true;
    }

    /// <summary>
    /// Coloca o Item segurado em um Slot do inventário
    /// </summary>
    /// <param name="targetSlot">Slot para colocar o Item segurado</param>
    public static void PlaceItem(Slot targetSlot)
    {
		HeldItem.Held = false;
		HeldItem.Image.raycastTarget = true;

		HeldItem.transform.SetParent(targetSlot.transform);
		HeldItem.transform.localPosition = Vector3.zero;

        targetSlot.Item = HeldItem;
        HeldItem = null;
	}
}
