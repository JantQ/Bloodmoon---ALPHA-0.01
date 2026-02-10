using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Build.Content;
using Unity.VectorGraphics;
using Unity.Hierarchy;

public class SaveSystem
{
    private static SaveData saveData = new SaveData();

    [System.Serializable]
    public struct SaveData
    {
        public ChildSaveData childSaveData;
    }

    public static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath + "/save" + ".save";
        return saveFile;
    }

    public static void Save()
    {
        HandleSaveData();

        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(saveData, true));
        Debug.Log(SaveFileName() + " Saved");
    }

    private static void HandleSaveData() 
    {
        GameObject.Find("Builder").GetComponent<SaveChildren>().Save(ref saveData.childSaveData);
    }

    public static void Load()
    {
        string saveContent = File.ReadAllText(SaveFileName());

        saveData = JsonUtility.FromJson<SaveData>(saveContent);

        HandleLoadData();
    }

    private static void HandleLoadData()
    {
        GameObject.Find("Builder").GetComponent<SaveChildren>().Load(saveData.childSaveData);
    }
}
