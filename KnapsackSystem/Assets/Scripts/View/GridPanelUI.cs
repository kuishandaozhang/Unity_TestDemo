using UnityEngine;
using System.Collections;

public class GridPanelUI : MonoBehaviour {
    public Transform[] Grids;
    /// <summary>
    /// 获取空格子以便存放 新的物品 或 另外格子的物品
    /// </summary>
    public Transform GetEmptyGrid()
    {
        for (int i = 0; i < Grids.Length; i++)
        {
            if (Grids[i].childCount == 0)
                return Grids[i];
        }
        return null;
    }
}
