using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HotbarManager : MonoBehaviour
{
	public static HotbarManager s_Instance;
	public static int s_SelectedSlot = 0;
	public static List<Slot> s_Slots = new List<Slot>();

	void Start()
	{
		if (s_Instance == null)
		{
			s_Instance = this;

			foreach(Transform slotTrs in transform)
			{
				s_Slots.Add(slotTrs.GetComponent<Slot>());
			}

			s_Slots[s_SelectedSlot].SetActive(true);
		}
		else
		{
			Destroy(this);
		}
	}

	void Update()
	{
		if (InventoryManager.Open)
		{
			return;
		}

		for(int key = (int)KeyCode.Alpha1; key <= (int)KeyCode.Alpha9; key ++)
		{
			if (Input.GetKeyDown((KeyCode)key))
			{
				GetSelectedSlot().SetActive(false);
				s_SelectedSlot = key - ((int)KeyCode.Alpha1);
				GetSelectedSlot().SetActive(true);
			}
		}
	}

	public static Slot GetSelectedSlot()
	{
		return s_Slots[s_SelectedSlot];
	}

	public static Slot GetSlotAvailable()
	{
		foreach(Slot slot in s_Slots)
		{
			if (slot.Item == null)
			{
				return slot;
			}
		}
		Debug.LogWarning("No Slot available found in the hotbar");
		return null;
	}
}
