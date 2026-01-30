using UnityEngine;

public class Validation : MonoBehaviour
{
    public bool valid = false;
    bool coliding = false;
    private void Update()
    {
        if (!coliding)
        {
            valid = true;
        }
        coliding = false;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            valid = false;
            coliding = true;
        }
    }
}
