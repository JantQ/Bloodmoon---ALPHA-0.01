using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SaveChildren : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            SaveSystem.Save();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SaveSystem.Load();
        }
    }

    public void Save(ref ChildSaveData data)
    {
        if (transform.childCount > 0)
        {
            data.names = new List<string>();
            Transform[] trans = transform.GetComponentsInChildren<Transform>();
            data.locations = new List<Vector3>();
            data.rotations = new List<Quaternion>();
            data.scale = new List<Vector3>();
            for (int i = 0; trans.Length > i; i++)
            {
                data.locations.Add(trans[i].position);
                data.rotations.Add(trans[i].rotation);
                data.scale.Add(trans[i].localScale);
            }
            Debug.Log(Convert.ToString(trans.Count()));
            for (int i = 0; trans.Length > i; i++)
            {
                data.names.Add(trans[i].name);
            }
        }
    }

    public void Load(ChildSaveData data)
    {
        GameObject builder = GameObject.Find("Builder");
        for (int i = 0; data.names.Count > i; i++)
        {
            for (int x = 0; x < builder.GetComponent<Builder>().buildings.Count; x++)
            {
                if (builder.GetComponent<Builder>().buildings[x].name + "(Clone)" == data.names[i])
                {
                    Instantiate(builder.GetComponent<Builder>().buildings[x], data.locations[x], data.rotations[x], transform);
                }
            }
        }
    }
}
[System.Serializable]
public struct ChildSaveData
{
    public List<Vector3> locations;
    public List<Quaternion> rotations;
    public List<Vector3> scale;
    public List<string> names;
}
