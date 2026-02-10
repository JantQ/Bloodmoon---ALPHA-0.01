using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage = 15f;
    public float attackCooldown = 1f;

    float lastAttackTime;

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            lastAttackTime = Time.time;
        }
    }

}
