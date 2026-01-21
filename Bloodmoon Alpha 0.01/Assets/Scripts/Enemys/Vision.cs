using UnityEngine;

public class Vision : MonoBehaviour
{
    public LayerMask mask;
    public bool vision(GameObject player, float range)
    {
        Debug.DrawRay(transform.position, player.transform.position - transform.position);
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out RaycastHit hit, range, mask))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
