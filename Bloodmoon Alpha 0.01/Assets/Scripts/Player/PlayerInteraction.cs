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
        // Create ray from camera forward
        Ray ray = new Ray(
            Camera.main.transform.position,
            Camera.main.transform.forward
        );

        // Visualize ray in editor
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 0.5f);

        // Raycast
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Debug.Log($"Hit object: {hit.collider.name}");

            // Look for BreakableObject in parent hierarchy
            BreakableObject breakable =
                hit.collider.GetComponentInParent<BreakableObject>();

            if (breakable != null)
            {
                Debug.Log("Tree hit.");
                breakable.TakeDamage(damage);
            }
            else
            {
                Debug.Log("Hit object is NOT breakable.");
            }
        }
        else
        {
            Debug.Log("No object hit.");
        }
    }
}