using Mono.Cecil.Cil;
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Builder : MonoBehaviour
{
    /// <summary>
    /// Onko pelaaja rakentamassa?
    /// </summary>
    public bool building = false;
    /// <summary>
    /// Rakennettavan asian esikatselu objecti
    /// </summary>
    public GameObject Ghoust;

    public Material valid;
    public Material invalid;
    /// <summary>
    /// Lista rakennettavia muotoja
    /// </summary>
    public List<GameObject> buildings;
    /// <summary>
    /// Mihin kerroksiin kelaaja voi rakentaa
    /// </summary>
    public LayerMask mask;
    /// <summary>
    /// Mit‰ pelaaja rakentaa numerona. Referoi buildings listaan
    /// </summary>
    public int build = 0;

    public int rotat = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) // Togle building mode
        {
            building = !building;
        }
        if (building)//Jos rakentamassa
        {
            if (Input.GetMouseButtonDown(1)) // jos painat oikeaa hiiren nappia vaihda rakennettavaa esinett‰ listan sis‰ll‰
            {
                build = (build + 1) % buildings.Count;
                Destroy(Ghoust);
            }
            if (Ghoust == null) // Jos haamu puuttuu, luo uusi haamu ja poista haamut colliderit
            {
                Ghoust = Instantiate(buildings[build]);
                Ghoust.GetComponentInChildren<Renderer>().material = valid;
                MeshCollider[] box = Ghoust.GetComponentsInChildren<MeshCollider>();
                for (int i = 0; i < box.Length; ++i)
                {
                    box[i].enabled = false;
                }
                box[0].transform.AddComponent<Validation>();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                rotat += 90;
            }
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 50f, mask, QueryTriggerInteraction.Ignore)) // Katso minne pelaaja katsoo ja tallenna raycast hitinfo
            {
                Snap(hit);
                Ghoust.transform.Rotate(0, rotat, 0);
                Ghoust.SetActive(true);
            }
            else { Ghoust.SetActive(false); }
            if (Ghoust.active && Input.GetMouseButtonDown(0) && Valid()) // Mik‰li haamun pystyy laittaa nykyiseen siaintiinsa ja pelaaja painaa vasenta hiiren nappia, luo uusi rakennelma valittua tyyppi‰ haamun kohdalle, "Builder"in lapsi objectina
            {
                Instantiate(buildings[build], Ghoust.transform.position, Ghoust.transform.rotation, transform);
            }
        }
        else if (Ghoust != null) // Jos ei rakentamassa ja haamu on olemassa, tuhoa haamu
        {
            Destroy(Ghoust);
            Ghoust = null;
        }
    }
    /// <summary>
    /// Jos Pystyy yhdist‰m‰‰n haamun katsottuun rakennelmaan, yhdist‰ se siihen. Jos ei ole rakennelma, laita mihin katsotaan 
    /// </summary>
    /// <param name="hit"></param>
    public void Snap(RaycastHit hit) // Ota ray niin tiet‰‰ mit‰ katsoo
    {
        if (hit.transform.tag == "Floor") // Jos katsot lattiaa, yhdisty siihen.
        {
            Vector3 dir = hit.transform.position - hit.point;
            dir.y = 0;
            Vector3 uplift = new Vector3();
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z)) // Katso mihin suuntaan yhdistyt, x vai z
            {
                dir.z = 0;
                if (dir.x < 0) // Katso kumpi reuna, positiivinen vai negatiivinen
                {
                    dir.x = 4 * Ghoust.transform.localScale.x / 100;
                }
                else
                {
                    dir.x = -4 * Ghoust.transform.localScale.x / 100;
                }
            }
            else
            {
                dir.x = 0;
                if (dir.z < 0) // Katso kumpi reuna, positiivinen vai negatiivinen
                {
                    dir.z = 4 * Ghoust.transform.localScale.x / 100;
                }
                else
                {
                    dir.z = -4 * Ghoust.transform.localScale.x / 100;
                }
            }
            if (Ghoust.tag == "Wall") // mik‰li yrit‰t laittaa sein‰‰, s‰‰d‰ siainti t‰ydellist‰ sn‰ppi‰ varten ja k‰‰nn‰ oikeaan suuntaan
            { 
                dir.z /= 2;
                dir.x /= 2;
                uplift.y = 2 * Ghoust.transform.localScale.x / 100;
                Ghoust.transform.position = hit.transform.position + dir + uplift;
                Vector3 target = hit.transform.position;
                target.y += 2 * Ghoust.transform.localScale.x / 100;
                Ghoust.transform.LookAt(target);
            }
            else // Jos et ole luomassa mit‰‰n edellisist‰, k‰yt‰ default
            {
                Ghoust.transform.rotation = hit.transform.rotation;
                Ghoust.transform.position = hit.transform.position + dir;
            }
        }
        else if (hit.transform.tag == "Wall") // jos katsoo sein‰‰ snap sein‰‰n
        {
            Vector3 dir = hit.transform.position - hit.point;
            Ghoust.transform.position = hit.transform.position + dir;
            if (Ghoust.tag == "Floor") // jos olet valinnut lattian, rajaa snap suunnat
            {
                if (dir.y > 0)
                {
                    dir = new Vector3();
                    dir.y = -2 * Ghoust.transform.localScale.x / 100;
                    dir += hit.transform.forward * 2 * Ghoust.transform.localScale.x / 100;
                }
                else
                {
                    dir = new Vector3();
                    dir.y = 2 * Ghoust.transform.localScale.x / 100;
                    dir += hit.transform.forward * 2 * Ghoust.transform.localScale.x / 100;
                }
                Ghoust.transform.position = hit.transform.position + dir;
            }
            else
            {
                if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z)) // valitse suunta mihin snap tapahtuu
                {
                    if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                    {
                        if (dir.x > 0)
                        {
                            dir = new Vector3();
                            dir.x = -4 * Ghoust.transform.localScale.x / 100;
                        }
                        else
                        {
                            dir = new Vector3();
                            dir.x = 4 * Ghoust.transform.localScale.x / 100;
                        }
                    }
                    else
                    {
                        if (dir.y > 0)
                        {
                            dir = new Vector3();
                            dir.y = -4 * Ghoust.transform.localScale.x / 100;
                        }
                        else
                        {
                            dir = new Vector3();
                            dir.y = 4 * Ghoust.transform.localScale.x / 100;
                        }
                    }
                }
                else
                {
                    if (Mathf.Abs(dir.y) < Mathf.Abs(dir.z))
                    {
                        if (dir.z > 0)
                        {
                            dir = new Vector3();
                            dir.z = -4 * Ghoust.transform.localScale.x / 100;
                        }
                        else
                        {
                            dir = new Vector3();
                            dir.z = 4 * Ghoust.transform.localScale.x / 100;
                        }
                    }
                    else
                    {
                        if (dir.y > 0)
                        {
                            dir = new Vector3();
                            dir.y = -4 * Ghoust.transform.localScale.x / 100;
                        }
                        else
                        {
                            dir = new Vector3();
                            dir.y = 4 * Ghoust.transform.localScale.x / 100;
                        }
                    }
                }
            }
            Ghoust.transform.rotation = hit.transform.rotation;
            Ghoust.transform.position = hit.transform.position + dir;
        }
        else // mik‰li et osu mihink‰‰n mihin snap toimii, laita mihin ray osoittaa
        {
            Ghoust.transform.rotation = new Quaternion();
            Ghoust.transform.position = hit.point;
        }
    }

    private bool Valid()
    {
        return true;
        Validation val = Ghoust.GetComponentInChildren<Validation>();
        bool valid_bool= val.IsValid(mask);
        if (true)
        {
            val.transform.GetComponent<Renderer>().material = valid;
        }
        else
        {
            val.transform.GetComponent<Renderer>().material = invalid;
        }
        return true;
    }
}
