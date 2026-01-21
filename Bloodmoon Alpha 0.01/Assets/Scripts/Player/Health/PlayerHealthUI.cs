using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image healthFill;

    public float smoothSpeed = 5f;
    float targetFill;

    void Update()
    {
        targetFill = playerHealth.HealthPercent();
        healthFill.fillAmount = Mathf.Lerp(
            healthFill.fillAmount,
            targetFill,
            Time.deltaTime * smoothSpeed
        );
    }
}