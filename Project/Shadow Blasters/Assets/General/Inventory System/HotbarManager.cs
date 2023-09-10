using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Controla a Hotbar no Inventory System
/// </summary>
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

		// Verifica pelo pressionamento de uma tela numeral (1-9) para seleção do Slot ativo
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

	/// <returns>Slot atualmente ativo</returns>
	public static Slot GetSelectedSlot()
	{
		return s_Slots[s_SelectedSlot];
	}

	/// <returns>O primeiro Slot disponível na Hotbar, null se a hotbar está cheia</returns>
	public static Slot GetSlotAvailable()
	{
		foreach(Slot slot in s_Slots)
		{
			if (slot.Item == null)
			{
				return slot;
			}
		}
		return null;
	}
}
