using UnityEngine;

public class Jumping : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private Rigidbody rb;
    public bool isOnGround = false;
    private bool isJumping = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isJumping = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Floor"))
        {
            isOnGround = true;

            if (animator != null)
                animator.SetBool("IsGrounded", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Floor"))
        {
            isOnGround = false;

            if (animator != null)
                animator.SetBool("IsGrounded", false);
        }
    }


    public void Jump(float jumpPower)
    {
        if (!isOnGround) return;

        isOnGround = false;

        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

        if (animator != null)
        {
            animator.SetTrigger("IsJumping");
        }

        if (Input.GetKey(KeyCode.R))
        {
            isJumping = false;
        }
    }
}
