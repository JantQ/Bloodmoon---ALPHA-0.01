using UnityEngine;

public class Vision : MonoBehaviour
{
    public bool vision(GameObject player, float range)
    {
        Debug.DrawRay(transform.position, player.transform.position - transform.position);
        if (Physics.Raycast(transform.position, player.transform.position - transform.position, out RaycastHit hit, range))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
