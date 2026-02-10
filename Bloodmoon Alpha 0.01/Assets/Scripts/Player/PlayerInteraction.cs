using UnityEngine;

/// <summary>
/// Handles player interactions with breakable objects (trees, rocks, etc.)
/// Supports multi-mesh objects using parent lookup
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    public int damage = 1;     // Damage per hit
    public float range = 2f;   // Interaction distance

    void Update()
    {
        // Left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            TryHit();
        }
    }

    void TryHit()
    {
        Item equipped = PlayerHotbarController.Instance.GetEquippedItem();

        if (equipped == null)
        {
            Debug.Log("No equipped item");
            return;
        }

        Debug.Log("Equipped tool: " + equipped.toolType);

        Ray ray = new Ray(
            Camera.main.transform.position,
            Camera.main.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            BreakableObject breakable =
                hit.collider.GetComponentInParent<BreakableObject>();

            if (breakable == null)
            {
                Debug.Log("Hit non-breakable");
                return;
            }

            Debug.Log("Hit breakable: " + breakable.breakType);

            if (!CanBreak(equipped.toolType, breakable.breakType))
            {
                Debug.Log("Wrong tool for this target");
                return;
            }

            breakable.TakeDamage(damage);
        }
    }

    bool CanBreak(ToolType tool, BreakType target)
    {
        if (tool == ToolType.Axe && target == BreakType.Tree)
            return true;

        if (tool == ToolType.Pickaxe && target == BreakType.Stone)
            return true;

        return false;
    }
}