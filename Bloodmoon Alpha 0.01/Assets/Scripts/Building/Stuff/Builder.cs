using Mono.Cecil.Cil;
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public bool building = false;

    public GameObject Ghoust;

    public List<GameObject> buildings;

    public LayerMask mask;

    public int build = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            building = !building;
        }
        if (building)
        {
            if (Input.GetMouseButtonDown(1))
            {
                build = (build + 1) % buildings.Count;
            }
            if (Ghoust == null)
            {
                Ghoust = Instantiate(buildings[build]);
                BoxCollider[] box = Ghoust.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < box.Length; ++i)
                {
                    box[i].enabled = false;
                }
            }
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 50f, mask))
            {
                snap(hit);
                Ghoust.SetActive(true);
            }
            else { Ghoust.SetActive(false); }
            if (Ghoust.active && Input.GetMouseButtonDown(0))
            {
                Instantiate(buildings[build], Ghoust.transform.position, Ghoust.transform.rotation, transform);
            }
        }
        else if (Ghoust != null)
        {
            Destroy(Ghoust);
            Ghoust = null;
        }
    }

    public void snap(RaycastHit hit)
    {
        if (hit.transform.tag == "Floor")
        {
            Vector3 dir = hit.transform.position - hit.point;
            dir.y = 0;
            Vector3 uplift = new Vector3();
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
            {
                dir.z = 0;
                if (dir.x < 0)
                {
                    dir.x = 4;
                }
                else
                {
                    dir.x = -4;
                }
            }
            else
            {
                dir.x = 0;
                if (dir.z < 0)
                {
                    dir.z = 4;
                }
                else
                {
                    dir.z = -4;
                }
            }
            if (Ghoust.name == "Wall(Clone)") 
            { 
                dir.z /= 2;
                dir.x /= 2;
                uplift.y = 2;
                Ghoust.transform.position = hit.transform.position + dir + uplift;
                Vector3 target = hit.transform.position;
                target.y += 2;
                Ghoust.transform.LookAt(target);
            }
            else
            {
                Ghoust.transform.position = hit.transform.position + dir;
            }
        }
        else
        {
            Ghoust.transform.position = hit.point;
        }
    }
}
