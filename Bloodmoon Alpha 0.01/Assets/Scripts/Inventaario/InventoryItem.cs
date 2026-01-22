using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    Image itemIcon;
    public CanvasGroup canvasGroup { get; private set; }

    public Item myItem { get; set; }
    public InventorySlot activeSlot { get; set; }

    [HideInInspector]
    public int count = 1; // New: count for stackable items
    public Text countText; // Assign a Text component in the prefab for displaying stack

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();
    }

    public void Initialize(Item item, InventorySlot parent)
    {
        activeSlot = parent;
        activeSlot.myItem = this;
        myItem = item;
        itemIcon.sprite = item.sprite;

        if (myItem.itemTag == SlotTag.Stackable)
        {
            count = 1;
            UpdateCountText();
        }
        else
        {
            if (countText != null)
                countText.text = "";
        }
    }

    public void AddStack(int amount)
    {
        count += amount;
        UpdateCountText();
    }

    public void UpdateCountText()
    {
        if (countText != null)
            countText.text = count > 1 ? count.ToString() : "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Inventory.Singleton.SetCarriedItem(this);
        }
    }
}