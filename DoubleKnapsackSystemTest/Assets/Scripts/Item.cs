using UnityEngine;
using System.Collections;

public enum ItemType    //武器 消耗品 防具
{
    WEAPON,
    CONSUMABLE,
    ARMOR
};

public class Item
{
    public string Name { get; private set; }
    public string Des { get; private set; }
    public ItemType ItemType { get; protected set; }

    public Item(string name, string des)
    {
        this.Name = name;
        this.Des = des;
    }
}
