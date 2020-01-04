using UnityEngine;
using System.Collections;
/// <summary>
/// 武器
/// </summary>
public class Weapon : Item
{
    public int Damage { get; private set; }

    public Weapon(string name, string des, int damage)
        : base(name, des)
    {
        this.Damage = damage;
        base.ItemType = global::ItemType.ARMOR;
    }
}
