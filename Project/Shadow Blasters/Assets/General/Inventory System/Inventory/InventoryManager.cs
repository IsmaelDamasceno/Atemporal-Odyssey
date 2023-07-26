using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	[SerializeField] private GameObject _slotItemPrefab;

    public static InventoryManager s_Instance;
    public static bool Open;

    private static HotbarManager _hotbar;
    private static List<Slot> s_invSlotList = new();

    void Start()
    {
        if (s_Instance == null)
        {
            s_Instance = this;

            _hotbar = HotbarManager.s_Instance;
			
            foreach (Transform slotTrs in transform)
			{
				s_invSlotList.Add(slotTrs.GetComponent<Slot>());
			}
		}
        else
        {
            Destroy(this);
        }
    }

	public static void CreateInvItem(Item item)
	{
		// Gets the first Slot available
		Slot availableSlot = GetSlotAvailable();
		// Check if it was found
		if (availableSlot != null)
		{
			// Instantiates and Initializes the item in said Slot
			Transform parentSlot = availableSlot.transform;
			Debug.Log("Instantiation");
			SlotItem newSlotItem = Instantiate(s_Instance._slotItemPrefab, parentSlot).GetComponent<SlotItem>();
			Debug.Log("Instantiated");
			newSlotItem.InitializeItem(item);
		}
	}
    public static Slot GetSlotAvailable()
    {
		#region Look for Available Hotbar Slot
		Slot hotbarSlot = HotbarManager.GetSlotAvailable();
		if (hotbarSlot != null)
		{
			return hotbarSlot;
		}
		#endregion

		#region Look for Available Inventory slot
		foreach(Slot slot in s_invSlotList)
		{
			if (slot.Item == null)
			{
				return slot;
			}
		}
		Debug.LogWarning("No Slot available found in Inventory");
		return null;
		#endregion
	}
}
