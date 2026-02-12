using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Singleton;
    public static InventoryItem carriedItem;

    [SerializeField] InventorySlot[] inventorySlots;
    [SerializeField] InventorySlot[] hotbarSlots;

    [SerializeField] InventorySlot[] equipmentSlots;

    [SerializeField] Transform draggablesTransform;
    public Transform DraggableRoot => draggablesTransform;
    [SerializeField] InventoryItem itemPrefab;

    [Header("Item List")]
    [SerializeField] Item[] items;

    [Header("Debug")]
    [SerializeField] Button giveItemBtn;


    private void Awake()
    {
        Singleton = this;
        giveItemBtn.onClick.AddListener(() => SpawnInventoryItem());
    }

    private void Update()
    {
        if (carriedItem != null)
            carriedItem.transform.position = Input.mousePosition;
    }

    public void SetCarriedItem(InventoryItem item)
    {
        if (carriedItem != null) return;

        InventorySlot fromSlot = item.activeSlot;

        if (fromSlot != null)
        {
            fromSlot.ClearSlot();
        }

        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggablesTransform);
    }

    public void EquipEquipment(SlotTag tag, InventoryItem item = null)
    {
        switch (tag)
        {
            case SlotTag.Head:
                if (item == null)
                    Debug.Log("Unequipped helmet on " + tag);
                else
                    Debug.Log("Equipped " + item.myItem.name + " on " + tag);
                break;
            case SlotTag.Chest: break;
            case SlotTag.Legs: break;
            case SlotTag.Feet: break;
        }
    }

    public void SpawnInventoryItem(Item item = null)
    {
        Item _item = item ?? PickRandomItem();

        // Merge stackable items if possible
        if (_item.itemTag == SlotTag.Stackable)
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                if (slot.myItem != null && slot.myItem.myItem == _item)
                {
                    int maxStack = 64;
                    int spaceLeft = maxStack - slot.myItem.count;

                    if (spaceLeft > 0)
                    {
                        slot.myItem.AddStack(1);
                        return; // merged successfully
                    }
                }
            }
        }

        // Otherwise, spawn in an empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].myItem == null)
            {
                InventoryItem newItem = Instantiate(itemPrefab, inventorySlots[i].transform);

                // Reset RectTransform to fill the slot
                RectTransform rt = newItem.GetComponent<RectTransform>();
                rt.anchorMin = Vector2.zero;
                rt.anchorMax = Vector2.one;
                rt.offsetMin = Vector2.zero;
                rt.offsetMax = Vector2.zero;
                rt.localScale = Vector3.one;

                newItem.Initialize(_item, inventorySlots[i]);

                // Place the item into the slot properly
                inventorySlots[i].SetItem(newItem);

                break;
            }
        }
    }

    private Item PickRandomItem()
    {
        int random = Random.Range(0, items.Length);
        return items[random];
    }
    public int HotbarCount => hotbarSlots.Length;

    public InventorySlot GetHotbarSlot(int index)
    {
        if (index < 0 || index >= hotbarSlots.Length)
            return null;

        return hotbarSlots[index];
    }
}