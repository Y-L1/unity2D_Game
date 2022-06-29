using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler 
{
    public Transform originalParent;
    public Inventory myBag;
    public int currentItemID;//当前物品ID

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        currentItemID = originalParent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        try
        {
            transform.position = eventData.position;
        }
        catch(NullReferenceException e)
        {
            transform.SetParent(originalParent);
            transform.position = originalParent.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        try
        {
            if (eventData.pointerCurrentRaycast.gameObject.name == "ItemImage")//交换位置
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;

                var temp = myBag.itemList[currentItemID];
                myBag.itemList[currentItemID] = myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;

                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            else if (eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)")//移动到空格
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;

                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = myBag.itemList[currentItemID];
                if(eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID != currentItemID)
                {
                    myBag.itemList[currentItemID] = null;
                }
                

                GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            else//移动到其他地方
            {
                transform.SetParent(originalParent);
                transform.position = originalParent.position;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }
        catch (NullReferenceException e)
        {
            transform.SetParent(originalParent);
            transform.position = originalParent.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        
    }

    private void FixedUpdate()
    {
    }


    public void UseItem()
    {

    }
}
