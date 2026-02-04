using UnityEngine;

public class Walking : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(float speed)
    {
        bool isWalking = false;

        Vector3 forward = transform.forward;      // because object is rotated 90°
        Vector3 right = -transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += forward * speed * Time.deltaTime;
            isWalking = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= forward * speed * Time.deltaTime;
            isWalking = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += right * speed * Time.deltaTime;
            isWalking = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position -= right * speed * Time.deltaTime;
            isWalking = true;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
           isWalking = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            isWalking = false;
        }

        animator.SetBool("IsWalking", isWalking);
    }
}
