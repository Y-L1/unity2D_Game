using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slot : MonoBehaviour
{
    public int slotID;       //空格ID，物品ID
    public Item slotItem;   //物品
    public Image slotImage; //图片
    public Text slotNum;    //数量
    public string slotInfo;

    public GameObject itemInSlot;

    public void ItemOnClicked()
    {
        InventoryManger.UpdateItemInfo(slotInfo);

        GameObject.FindGameObjectWithTag("Use").GetComponent<UseItem>().GetCurrentItem(this);
    }

    public void SetupSlot(Item item)
    {
        if(item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }
        slotImage.sprite = item.itemImage;
        slotNum.text = item.itemHeld.ToString();
        slotInfo = item.itemInfo;

        slotItem = item;

    }
}
