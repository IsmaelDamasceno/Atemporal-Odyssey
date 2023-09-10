using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla o armazenamento e ações do Inventory System (slots, criar item, achar slot disponível, etc)
/// </summary>
public class InventoryManager : MonoBehaviour
{
	[SerializeField] private GameObject _slotItemPrefab;

    public static InventoryManager s_Instance;
    public static bool Open;

    private static HotbarManager _hotbar;
    private static List<Slot> s_invSlotList = new();


	/// <summary>
	/// Seta o estado do baú (aberto/fechado)
	/// </summary>
	/// <param name="val">Novo estado: false (fechado), true (aberto)</param>
	public static void SetInventory(bool val)
	{
		if (val)
		{
			OpenInventory();
		}
		else
		{
			CloseInventory();
		}
	}

	/// <summary>
	/// Inverte o estado do baú (se aberto, fecha, se fechado, abre)
	/// </summary>
	public static void SetInventory()
	{
		if (Open)
		{
			CloseInventory();
		}
		else
		{
			OpenInventory();
		}
	}

	/// <summary>
	/// Abre o inventário
	/// </summary>
	public static void OpenInventory()
	{
		InventoryAnimations.s_Instance.StartCoroutine(InventoryAnimations.s_Instance.OpenCloseCoroutine(1));
		Open = true;

		HotbarManager.GetSelectedSlot().SetActive(false);
	}

	/// <summary>
	/// Fecha o inventário
	/// </summary>
	public static void CloseInventory()
	{
		InventoryAnimations.s_Instance.StartCoroutine(InventoryAnimations.s_Instance.OpenCloseCoroutine(0));
		Open = false;

		HotbarManager.GetSelectedSlot().SetActive(true);
	}

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

	/// <summary>
	/// Cria um Item no primeiro Slot disponível encontrado
	/// </summary>
	/// <param name="item">Item para criar</param>
	public static void CreateInvItem(Item item)
	{
		// Gets the first Slot available
		Slot availableSlot = GetSlotAvailable();
		// Check if it was found
		if (availableSlot != null)
		{
			// Instantiates and Initializes the item in said Slot
			Transform parentSlot = availableSlot.transform;
			SlotItem newSlotItem = Instantiate(s_Instance._slotItemPrefab, parentSlot).GetComponent<SlotItem>();
			newSlotItem.InitializeItem(item);
		}
	}

	/// <summary>
	/// Procura pelo primeiro Slot disponível encontrado
	/// </summary>
	/// <returns>Referência ao Slot encontrado, null caso o inventário esteja cheio</returns>
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
		return null;
		#endregion
	}
}
