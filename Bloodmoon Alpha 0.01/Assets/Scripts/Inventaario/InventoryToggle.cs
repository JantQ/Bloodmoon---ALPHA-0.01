using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryUI;
    public KeyCode toggleKey = KeyCode.I;

    private bool isOpen;

    void Start()
    {
        inventoryUI.SetActive(false);
        LockCursor();
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if (isOpen)
                CloseInventory();
            else
                OpenInventory();
        }
    }

    void OpenInventory()
    {
        isOpen = true;
        inventoryUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f; // optional
    }

    void CloseInventory()
    {
        isOpen = false;
        inventoryUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f; // optional
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}