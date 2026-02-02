using UnityEngine;

public class Vision : MonoBehaviour
{
    public LayerMask mask;
    /// <summary>
    /// Enemy tarkistaa voiko nähdä pelaajan (player) törmäämättä mihinkään.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="range"></param>
    /// <returns></returns>
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
