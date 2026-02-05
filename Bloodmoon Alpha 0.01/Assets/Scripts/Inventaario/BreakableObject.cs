using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 5;
    public int currentHealth = 5;

    [Header("Drops")]
    public Item dropItem;          // ScriptableObject (Wood, Stone, etc.)
    public int minDrops = 1;
    public int maxDrops = 3;

    [Header("Drop Settings")]
    public GameObject worldItemPrefab; // Prefab with WorldItemPickup
    public float dropRadius = 0.5f;


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Break();
        }
    }

    private void Break()
    {
        int dropCount = Random.Range(minDrops, maxDrops + 1);

        for (int i = 0; i < dropCount; i++)
        {
            Vector3 offset = Random.insideUnitSphere * dropRadius;
            offset.y = 0f;

            GameObject drop = Instantiate(
                worldItemPrefab,
                transform.position + offset,
                Quaternion.identity
            );

            WorldItemPickup pickup = drop.GetComponent<WorldItemPickup>();
            pickup.item = dropItem;
        }

        Destroy(gameObject);
    }
}