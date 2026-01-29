using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage = 15f;
    public float attackCooldown = 1f;

    float lastAttackTime;

    void OnTriggerEnter(Collider other)
    {
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            lastAttackTime = Time.time;
        }
    }
}
