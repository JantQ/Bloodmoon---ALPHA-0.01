using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SaveChildren : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

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
            data.Components = new List<string>();
            Transform[] trans = transform.GetComponentsInChildren<Transform>();
            for (int i = 0; trans.Length > i; i++)
            {
                data.locations.add(trans[i]);
            }
            Debug.Log(Convert.ToString(trans.Count()));
            for (int i = 0; trans.Length > i; i++)
            {
                data.Components.Add("");
                foreach (Component comp in transform)
                {
                    string dat = comp.name;
                    data.Components[i] = dat;
                }
                Debug.Log(data.Components[i].Length);
            }
            Debug.Log(data.Components.Count);
        }
    }

    public void Load(ChildSaveData data)
    {

    }
}
[System.Serializable]
public struct ChildSaveData
{
    public List<Vector3> locations;
    public List<Vector3> rotations;
    public List<Vector3> scale;
    public List<string> Components;
}
