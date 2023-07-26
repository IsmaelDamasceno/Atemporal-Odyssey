using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventoryMouse
{
    public static SlotItem HeldItem;

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
