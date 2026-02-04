using UnityEngine;

public class Running : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public float run()
    {
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speedMultiplier = isRunning ? 2.5f : 1f;

        if (animator != null)
        {
            animator.SetBool("IsRunning", isRunning);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("IsRunning", false);
        }

        return speedMultiplier;
    }

}
