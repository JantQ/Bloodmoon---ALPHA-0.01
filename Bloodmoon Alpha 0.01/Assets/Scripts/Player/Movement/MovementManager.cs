using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public float moveSpeed = 2;

    public float jumpPower = 250;

    private Walking move;

    private Jumping jump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        move = GetComponent<Walking>();
        jump = GetComponent<Jumping>();
    }

    // Update is called once per frame
    void Update()
    {
        move.Move(moveSpeed);
        jump.Jump(jumpPower);
    }
}
