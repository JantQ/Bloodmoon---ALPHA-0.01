using TMPro;
using UnityEngine;

public class RainbowText : MonoBehaviour
{
    public float speed = 1f; // how fast the rainbow cycles
    public float saturation = 1f;
    public float value = 1f;

    private TextMeshProUGUI tmpText;

    void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Shift hue based on time
        float hue = (Time.time * speed) % 1f;
        Color rainbow = Color.HSVToRGB(hue, saturation, value);
        tmpText.color = rainbow;
    }
}