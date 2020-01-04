using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 拥有的物品集合
/// </summary>
public class ItemModel
{
    private static Dictionary<string, Item> GridItem = new Dictionary<string, Item>();
    /// <summary>
    /// 保存物品信息到集合里 [获得物品时]
    /// </summary>
    public static void StoreItem(string name, Item item)
    {
        if (GridItem.ContainsKey(name))
            DeleteItem(name);

        GridItem.Add(name, item);
    }
    /// <summary>
    /// 读取指定已有物品
    /// </summary>
    public static Item GetItem(string name)
    {
        if (GridItem.ContainsKey(name))
            return GridItem[name];
        return null;
    }
    /// <summary>
    /// 删除指定已有物品
    /// </summary>
    public static void DeleteItem(string name)
    {
        if (GridItem.ContainsKey(name))
            GridItem.Remove(name);
    }
}
