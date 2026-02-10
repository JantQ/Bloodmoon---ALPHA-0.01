using UnityEngine;
using UnityEngine.InputSystem;

public class MovementManager : MonoBehaviour
{
    public float moveSpeed = 2;
    public float speedMultiplier = 2.5f;
    public float sensitivity = 2;

    public float jumpPower = 250;

    private Walking move;
    private Running run;
    private Camera camera;
    private Jumping jump;
    private Crouching crouching;

    void Start()
    {
        move = GetComponent<Walking>();
        jump = GetComponent<Jumping>();
        run = GetComponent<Running>();
        camera = GetComponentInChildren<Camera>();
        crouching = GetComponent<Crouching>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump.Jump(jumpPower);
        }

        speedMultiplier = run.run();
        speedMultiplier = crouching.crouch(speedMultiplier);
        move.Move(moveSpeed * speedMultiplier);
    }
}
