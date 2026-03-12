using UnityEngine;
using System;

public class IDamageable : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] protected float health = 50f;
    [SerializeField] protected float maxHealth = 50f;

    [Header("Effects")]
    [SerializeField] protected ParticleSystem Blood;

    public static Action OnPlayerDeath;

    protected virtual void DealDamage(float dmg, GameObject target)
    {
        if (target.TryGetComponent(out IDamageable damageable))
        {
            Debug.Log($"Dealing {dmg} damage to {target.name}");
            damageable.TakeDamage(dmg);
        }
    }

    protected virtual void TakeDamage(float dmg)
    {
        health = Mathf.Clamp(health - dmg, 0f, maxHealth);

        Instantiate(Blood, transform.position, Quaternion.identity);

        Debug.Log($"{gameObject.name} took {dmg} damage. Remaining health: {health}/{maxHealth}");

        if (health <= 0f)
            Die();
    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} has died.");

        if (CompareTag("Player"))
        {
            OnPlayerDeath?.Invoke();
        }
    }
}