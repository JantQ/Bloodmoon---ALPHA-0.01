using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WorldItemPickup : MonoBehaviour
{
    public Item item;

    private bool pickedUp = false;

    private void Awake()
    {
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pickedUp) return;
        if (!other.CompareTag("Player")) return;

        if (item == null)
        {
            Debug.LogError("WorldItemPickup has no Item assigned!", this);
            return;
        }

        pickedUp = true;

        DisableObject();

        Inventory.Singleton.SpawnInventoryItem(item);

        Destroy(gameObject);
    }

    void DisableObject()
    {
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Renderer rend = GetComponent<Renderer>();
        if (rend != null) rend.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.linearVelocity = Vector3.zero;
    }
}