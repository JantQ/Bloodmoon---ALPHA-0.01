using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WorldItemPickup : MonoBehaviour
{
    public Item item; // Assign the ScriptableObject in the inspector

    private void Awake()
    {
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (item == null)
        {
            Debug.LogError("WorldItemPickup has no Item assigned!", this);
            return;
        }

        Inventory.Singleton.SpawnInventoryItem(item);
        Destroy(gameObject);
    }
}