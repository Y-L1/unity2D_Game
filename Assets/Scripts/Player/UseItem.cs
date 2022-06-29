using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
    [SerializeField]
    private GameObject Grid;

    private Slot currentSlot;

    [SerializeField] private Inventory myBag;
    void Start()
    {

    }

    private void FixedUpdate()
    {
        
    }

    public void GetCurrentItem(Slot nowSlot)
    {
        currentSlot = nowSlot;
    }

    public void UseBtnOnClicked()
    {
        if (currentSlot == null) return;

        

        string str = currentSlot.GetComponent<Slot>().slotItem.itemName;
        if (str.Equals("ÑªÆ¿"))
        {
            //¼ÓÑª
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthControll>().AddHealth();
        }
        if (str.Equals("À¶Æ¿"))
        {
            //¼ÓÀ¶
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthControll>().AddEnergy();
        }

        currentSlot.GetComponent<Slot>().slotItem.itemHeld--;

        for (int i = 0; i < myBag.itemList.Count; i++)
        {
            if (myBag.itemList[i] == currentSlot.slotItem)
            {
                if (currentSlot.slotItem.itemHeld <= 0)
                {
                    currentSlot.slotItem.itemHeld = 0;
                    myBag.itemList[i] = null;
                }
            }
        }

        InventoryManger.RefreshItem();
    }
}
