using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slot : MonoBehaviour
{
    public int slotID;       //�ո�ID����ƷID
    public Item slotItem;   //��Ʒ
    public Image slotImage; //ͼƬ
    public Text slotNum;    //����
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