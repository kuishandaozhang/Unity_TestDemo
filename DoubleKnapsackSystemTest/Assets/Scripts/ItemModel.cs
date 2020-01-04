using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemModel : MonoBehaviour
{
    public Dictionary<int, Item> itemList;
    public Dictionary<string, Item> itemListInfo;

    void Awake()
    {  
        Load();
    }
    //事先加载物品
    private void Load()
    {
        itemList = new Dictionary<int, Item>();
        Weapon w1 = new Weapon("牛刀", "除了牛逼还是牛逼", 410);
        Weapon w2 = new Weapon("羊刀", "伤害最低的武器，但勉强能用", 320);
        Weapon w3 = new Weapon("宝剑", "传说是当年皇帝派优秀工匠铸造而成，威力巨大", 1000);
        Weapon w4 = new Weapon("军枪", "远程武器，本身威力巨大，在运用适当走位时能发挥出最大威力", 50000);

        Consumable c1 = new Consumable("红瓶", "能给你回复200血量", 200);
        Consumable c2 = new Consumable("大红瓶", "比起红瓶能回更多的血量，当回复速度较慢", 1000);

        Armor a1 = new Armor("头盔", "保护你的头部，免收伤害", 500);
        Armor a2 = new Armor("护肩", "除了增加防御力没啥卵用", 300);
        Armor a3 = new Armor("胸甲", "能最大程度减少受到的伤害", 1000);
        Armor a4 = new Armor("护腿", "增加防御力，但会是移动速度大大降低", 800);

        itemList.Add(0, w1);
        itemList.Add(1, w2);
        itemList.Add(2, w3);
        itemList.Add(3, w4);
        itemList.Add(4, c1);
        itemList.Add(5, c2);
        itemList.Add(6, a1);
        itemList.Add(7, a2);
        itemList.Add(8, a3);
        itemList.Add(9, a4);

        itemListInfo = new Dictionary<string, Item>();
        itemListInfo.Add(w1.Name, w1);
        itemListInfo.Add(w2.Name, w2);
        itemListInfo.Add(w3.Name, w3);
        itemListInfo.Add(w4.Name, w4);
        itemListInfo.Add(c1.Name, c1);
        itemListInfo.Add(c2.Name, c2);
        itemListInfo.Add(a1.Name, a1);
        itemListInfo.Add(a2.Name, a2);
        itemListInfo.Add(a3.Name, a3);
        itemListInfo.Add(a4.Name, a4);
    }
}
