using UnityEngine;
using System.Collections;
/// <summary>
/// 消耗品
/// </summary>
public class Consumable : Item
{
    public int HP { get; private set; }

    public Consumable(string name, string des, int hp)
        : base(name, des)
    {
        this.HP = hp;
        base.ItemType = global::ItemType.ARMOR;
    }
}
