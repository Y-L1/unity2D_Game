using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/New Item")]

public class Item : ScriptableObject
{
    public string itemName;         //名字
    public Sprite itemImage;        //图片
    public int itemHeld;            //数量
    public bool isEquip;            //是否可装备
    [TextArea]
    public string itemInfo;         //描述


}
