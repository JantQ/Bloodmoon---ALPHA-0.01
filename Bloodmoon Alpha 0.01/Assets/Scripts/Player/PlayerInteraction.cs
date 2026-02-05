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

        // Must have an axe equipped
        if (equipped == null || equipped.equipmentPrefab == null)
            return;

        Ray ray = new Ray(
            Camera.main.transform.position,
            Camera.main.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            BreakableObject breakable =
                hit.collider.GetComponentInParent<BreakableObject>();

            if (breakable != null)
            {
                breakable.TakeDamage(damage);
            }
        }
    }
}