using UnityEngine;

public class Validation : MonoBehaviour
{
    public bool IsValid(LayerMask mask)
    {
        Vector3 start = transform.position + (transform.localScale / 2);
        Vector3 direction = (transform.position - start) * 2;
        Debug.DrawRay(start, direction, Color.rebeccaPurple, 10000f);
        Physics.Raycast(start, direction, out RaycastHit hit, 100000f, mask);//Se ei toimi rotaation kanssa koska me katsomme vääriä tietoja.

        start.x -= transform.localScale.x;
        direction = (transform.position - start) * 2;
        Debug.DrawRay(start, direction, Color.rebeccaPurple, 10000f);
        Physics.Raycast(start, direction, out RaycastHit hit2, 100000f, mask);//ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ!

        return true;
    }
}
