using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem myItem {  get; set; }
    public SlotTag myTag;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        // No carried item → nothing to place
        if (Inventory.carriedItem == null)
            return;

        // Slot tag restriction (equipment slots, etc.)
        if (myTag != SlotTag.None &&
            Inventory.carriedItem.myItem.itemTag != myTag)
            return;

        SetItem(Inventory.carriedItem);
    }
    public void SetItem(InventoryItem item)
    {
        myItem = item;

        item.transform.SetParent(transform, false);

        RectTransform rt = item.GetComponent<RectTransform>();

        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        rt.localScale = Vector3.one;

        item.activeSlot = this;
        InventorySlot fromSlot = item.activeSlot;
        InventoryItem itemInThisSlot = myItem;

        // STACKING
        if (itemInThisSlot != null &&
             itemInThisSlot != item &&
            itemInThisSlot.myItem.itemTag == SlotTag.Stackable &&
            itemInThisSlot.myItem == item.myItem)
        {
            int total = itemInThisSlot.count + item.count;
            int maxStack = 100;

            if (total <= maxStack)
            {
                itemInThisSlot.AddStack(item.count);
                Destroy(item.gameObject);
                Inventory.carriedItem = null;
            }
            else
            {
                itemInThisSlot.count = maxStack;
                itemInThisSlot.UpdateCountText();
                item.count = total - maxStack;
                item.UpdateCountText();
            }
            return;
        }

        // PLACE / SWAP LOGIC

        // Put carried item into this slot
        myItem = item;
        item.activeSlot = this;
        item.transform.SetParent(transform);
        item.canvasGroup.blocksRaycasts = true;

        // If item came from a slot
        if (fromSlot != null)
        {
            // Swap back the previous item (if any)
            fromSlot.myItem = itemInThisSlot;

            if (itemInThisSlot != null)
            {
                itemInThisSlot.activeSlot = fromSlot;
                itemInThisSlot.transform.SetParent(fromSlot.transform);
                itemInThisSlot.canvasGroup.blocksRaycasts = true;
            }
        }

        Inventory.carriedItem = null;

        if (myTag != SlotTag.None)
            Inventory.Singleton.EquipEquipment(myTag, myItem);
    }
}
