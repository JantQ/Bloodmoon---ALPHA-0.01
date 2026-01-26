using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    [Header("UI References")]
    [SerializeField] private Image itemIcon;    // Assign in prefab
    [SerializeField] private Text countText;    // Assign in prefab

    public CanvasGroup canvasGroup { get; private set; }

    public Item myItem { get; set; }
    public InventorySlot activeSlot { get; set; }

    [HideInInspector]
    public int count = 1; // For stackable items

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Initialize(Item item, InventorySlot parent)
    {
        activeSlot = parent;
        activeSlot.myItem = this;

        myItem = item;

        // Assign correct sprite
        if (itemIcon != null && myItem != null)
            itemIcon.sprite = myItem.sprite;

        // Handle stackable count
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