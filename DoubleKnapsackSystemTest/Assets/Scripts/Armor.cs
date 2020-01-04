using UnityEngine;
using System.Collections;
/// <summary>
/// 防具
/// </summary>
public class Armor : Item
{
    public int Defend { get; private set; }

    public Armor(string name, string des, int defend)
        : base(name, des)
    {
        this.Defend = defend;
        base.ItemType = global::ItemType.CONSUMABLE;
    }
}
