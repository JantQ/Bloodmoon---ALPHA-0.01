using UnityEngine;

public class Crouching : MonoBehaviour
{
    public float crouch(float speedMultiplier)
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            speedMultiplier = 0.5f;
        }
        return speedMultiplier;
    }
}
