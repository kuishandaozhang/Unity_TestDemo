using UnityEngine;
using System.Collections;

public class Item
{
    //物品属性
    public int ID { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int BuyPrice { get; private set; }
    public int SellPrice { get; private set; }
    public string Icon { get; private set; }
    //物品类型
    public string ItemType { get; protected set; }//设置成枚举类型更佳.

    public Item(int id, string name, string description, int buyPrice, int sellPrice, string icon)
    {
        this.ID = id;
        this.Name = name;
        this.Description = description;
        this.BuyPrice = buyPrice;
        this.SellPrice = sellPrice;
        this.Icon = icon;
    }
}
