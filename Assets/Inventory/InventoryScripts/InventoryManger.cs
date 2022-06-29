using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManger : MonoBehaviour
{
    static InventoryManger instance;
    public Inventory myBag;
    public GameObject slotGrid;
    //public Slot slotPrefeb;

    public GameObject emptySlot;
    public Text itemInformation;

    public List<GameObject> slots = new List<GameObject>();

    private void Start()
    {
        
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void OnEnable()
    {
        //刷新背包
        RefreshItem();
        //物品描述
        instance.itemInformation.text = "";

    }

    public static void UpdateItemInfo(string itemDescription)
    {
        instance.itemInformation.text = itemDescription;
    }

    public static void RefreshItem()
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
                break;

            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }
        for (int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            
            instance.slots.Add(Instantiate(instance.emptySlot));
            instance.slots[i].transform.SetParent(instance.slotGrid.transform);
            instance.slots[i].GetComponent<Slot>().slotID = i;
            instance.slots[i].GetComponent<Slot>().SetupSlot(instance.myBag.itemList[i]);
        }
        
    }
    
}
