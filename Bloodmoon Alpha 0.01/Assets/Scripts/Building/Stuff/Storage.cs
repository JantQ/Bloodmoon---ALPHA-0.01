using System;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{
    GameObject storageUI;
    bool saved = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private class itemInfo
    {
        public string item;
        public int number;
        public string slot;
    }
    private List<itemInfo> storage = new List<itemInfo>();
    public int capasity;

    public void StorageSave()
    {
        if (saved) {  return; }
        storageUI.SetActive(true);
        foreach (Transform t in storageUI.transform) 
        {
            t.gameObject.SetActive(true);
        }
        storage.Clear();
        saved = true;
        foreach (InventoryItem item in storageUI.GetComponentsInChildren<InventoryItem>())
        {
            itemInfo info = new itemInfo();

            info.item = item.myItem.name;
            if (item.GetComponentInChildren<Text>().text.Length > 0) 
            { info.number = Convert.ToInt16(item.GetComponentInChildren<Text>().text); }
            else { info.number = 0; }
            info.slot = item.GetComponentInParent<InventorySlot>().transform.name;

            storage.Add(info);
        }
        foreach (Transform t in storageUI.transform)
        {
            t.gameObject.SetActive(false);
        }
        storageUI.SetActive(false);
    }

    public void StorageLoad()
    {
        if (storageUI == null) { storageUI = GameObject.Find("Storage"); }
        saved = false;
        foreach(Transform child in storageUI.transform)
        {
            if (child.GetComponentInChildren<InventoryItem>() != null)
            {
                Destroy(child.GetComponentInChildren<InventoryItem>().gameObject);
            }
        }

        for (int i = 0; i < storage.Count; i++)
        {
            GameObject item = Instantiate(storageUI.GetComponentInParent<SaveMyStuff>().ItemPrefab, GameObject.Find(storage[i].slot).transform);
            for (int j = 0; j < storageUI.GetComponentInParent<SaveMyStuff>().items.Length; j++)
            {
                if (storage[i].item == storageUI.GetComponentInParent<SaveMyStuff>().items[j].name)
                {
                    item.GetComponent<InventoryItem>().myItem = storageUI.GetComponentInParent<SaveMyStuff>().items[j];
                    Debug.Log(storage[i].number);
                    if (storage[i].number > 1)
                    {
                        item.GetComponent<InventoryItem>().count = storage[i].number;
                        Debug.Log(item.GetComponent<InventoryItem>().count);
                        item.GetComponentInChildren<Text>().text = Convert.ToString(storage[i].number);
                    }
                    else
                    {
                        item.GetComponentInChildren<Text>().text = "";
                    }
                }
            }
            item.GetComponentInParent<InventorySlot>().SetItem(item.GetComponent<InventoryItem>());
            Debug.Log(item.GetComponent<InventoryItem>().count);
            item.GetComponent<Image>().sprite = item.GetComponent<InventoryItem>().myItem.sprite;
        }
    }
}
