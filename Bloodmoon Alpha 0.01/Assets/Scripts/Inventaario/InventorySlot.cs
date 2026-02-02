using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem myItem {  get; set; }
    public SlotTag myTag;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (Inventory.carriedItem == null) return;
            if (myTag != SlotTag.None && Inventory.carriedItem.myItem.itemTag != myTag) return;
            SetItem(Inventory.carriedItem);
        }
    }
    public void SetItem(InventoryItem item)
    {
        // If slot has the same stackable item, combine stacks
        if (myItem != null && myItem.myItem.itemTag == SlotTag.Stackable &&
            myItem.myItem == item.myItem)
        {
            int total = myItem.count + item.count;
            int maxStack = 100;

            if (total <= maxStack)
            {
                myItem.AddStack(item.count);
                Destroy(item.gameObject); // merged into existing stack
            }
            else
            {
                myItem.count = maxStack;
                myItem.UpdateCountText();
                item.count = total - maxStack; // remaining items stay in carriedItem
                item.UpdateCountText();
                Inventory.carriedItem = item; // still carrying leftover
            }
            return;
        }

        // Move item normally
        Inventory.carriedItem = null;
        if (item.activeSlot != null)
            item.activeSlot.myItem = null;

        myItem = item;
        myItem.activeSlot = this;
        myItem.transform.SetParent(transform);
        myItem.canvasGroup.blocksRaycasts = true;

        if (myTag != SlotTag.None)
        {
            Inventory.Singleton.EquipEquipment(myTag, myItem);
        }
    }
}
