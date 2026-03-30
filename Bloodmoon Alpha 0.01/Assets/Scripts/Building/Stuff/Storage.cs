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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private class itemInfo
    {
        public string item;
        public int number;
        public string slot;
    }
    private List<itemInfo> storage = new List<itemInfo>();
    public int capasity;
    void Start()
    {
        storageUI = GameObject.Find("Storage");
    }

    public void StorageSave()
    {
        storage.Clear();

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
    }

    public void StorageLoad()
    {
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
                    if (storage[i].number > 1)
                    {
                        item.GetComponentInChildren<Text>().text = Convert.ToString(storage[i].number);
                    }
                    else
                    {
                        item.GetComponentInChildren<Text>().text = "";
                    }
                }
            }
            RectTransform rt = item.GetComponent<RectTransform>();
            item.GetComponentInParent<InventorySlot>().SetItem(item.GetComponent<InventoryItem>());
            item.GetComponent<Image>().sprite = item.GetComponent<InventoryItem>().myItem.sprite;
        }
    }
}
