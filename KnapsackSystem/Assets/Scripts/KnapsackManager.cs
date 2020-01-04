using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class KnapsackManager : MonoBehaviour {
    private static KnapsackManager _instance;
    public static KnapsackManager Instance { get{return _instance;} }
    public GridPanelUI GridPanelUI;
    public TooltipUI TooltipUI;
    public DragItemUI DragItemUI;

    private bool isDrag = false;
    private bool isShow = false;

    public Dictionary<int, Item> ItemList;  //可能得到的物品的集合

    void Awake()
    {
        //单例
        _instance = this;
        //所有物品数据（含未拥有物品）
        Load();
        //事件
        GridUI.OnEnter += GridUI_OnEnter; //鼠标进入格子
        GridUI.OnExit += GridUI_OnExit;   //鼠标移出格子
        GridUI.OnLeftBeginDrag += GridUI_OnLeftBeginDrag; //开始拖拽物品
        GridUI.OnLeftEndDrag += GridUI_OnLeftEndDrag;     //结束拖拽物品
    }

    void Update()
    {
        Vector2 position; //拖拽物品跟随鼠标移动
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("KnapsackUI").transform as RectTransform, Input.mousePosition, null, out position); //鼠标坐标转换成UI坐标

        if (isDrag) //拖拽
        {
            DragItemUI.Show();
            DragItemUI.SetLocalPosition(position);
        }
        else if (isShow) //显示物品信息卡
        {
            TooltipUI.Show();
            TooltipUI.SetLocalPosition(position);
        }
    }
    /// <summary>
    /// 根据 ID [0-9] 得到物品并保存到背包里
    /// </summary>
    public void StoreItem(int itemId)
    {
        if (!ItemList.ContainsKey(itemId))
            return;

        Transform emptyGrid = GridPanelUI.GetEmptyGrid();
        if (emptyGrid == null)
        {
            Debug.LogWarning("背包已满!!"); //后期改成UI显示
            return;
        }

        Item temp = ItemList[itemId];
        this.CreatNewItem(temp, emptyGrid);
    }
    /// <summary>
    /// 预先保存游戏所有物品到 ItemList 集合里
    /// </summary>
    private void Load()
    { 
        ItemList = new Dictionary<int,Item>();
        Weapon w1 = new Weapon(0, "牛刀", "", 0, 0, "", 0);
        Weapon w2 = new Weapon(1, "羊刀", "", 0, 0, "", 0);
        Weapon w3 = new Weapon(2, "宝剑", "", 0, 0, "", 0);
        Weapon w4 = new Weapon(3, "军枪", "", 0, 0, "", 0);

        Consumable c1 = new Consumable(4, "红瓶", "", 0, 0, "", 0, 0);
        Consumable c2 = new Consumable(5, "蓝瓶", "", 0, 0, "", 0, 0);

        Armor a1 = new Armor(6, "头盔", "", 0, 0, "", 0, 0, 0);
        Armor a2 = new Armor(7, "护肩", "", 0, 0, "", 0, 0, 0);
        Armor a3 = new Armor(8, "胸甲", "", 0, 0, "", 0, 0, 0);
        Armor a4 = new Armor(9, "护腿", "", 0, 0, "", 0, 0, 0);

        ItemList.Add(w1.ID, w1);
        ItemList.Add(w2.ID, w2);
        ItemList.Add(w3.ID, w3);
        ItemList.Add(w4.ID, w4);
        ItemList.Add(c1.ID, c1);
        ItemList.Add(c2.ID, c2);
        ItemList.Add(a1.ID, a1);
        ItemList.Add(a2.ID, a2);
        ItemList.Add(a3.ID, a3);
        ItemList.Add(a4.ID, a4);
    }

    #region 事件回调
    /// <summary>
    /// 当鼠标进入 有物品的格子 时更新显示信息卡
    /// </summary>
    private void GridUI_OnEnter(Transform gridTransform)
    {
        if (gridTransform.childCount == 0) return;
        Item item = ItemModel.GetItem(gridTransform.name);
        if (item == null)
            return;
        string text = GetTooltipText(item);
        TooltipUI.UpdateTooltip(text);
        isShow = true;
    }
    /// <summary>
    /// 鼠标移出 隐藏信息卡
    /// </summary>
    private void GridUI_OnExit()
    {
        isShow = false;
        TooltipUI.Hide();
    }

    private void GridUI_OnLeftBeginDrag(Transform gridTransform)
    {
        if (gridTransform.childCount == 0) //空格子
            return;
        else //非空
        {
            Item item = ItemModel.GetItem(gridTransform.name);
            DragItemUI.UpdateItem(item.Name);
            Destroy(gridTransform.GetChild(0).gameObject);
            isDrag = true;
        }
    }

    private void GridUI_OnLeftEndDrag(Transform prevTransform,  Transform enterTransform)
    {
        isDrag = false;
        DragItemUI.Hide();

        if(enterTransform == null)//扔东西
        {
            ItemModel.DeleteItem(prevTransform.name);
            Debug.LogWarning("物品已扔"); //后期改为UI显示
        }
        else if(enterTransform.tag == "Grid")//拖到另一个格子里
        {
            if (enterTransform.childCount == 0)//[另外的空格子] 或者 [始格子 和 终格子 相同]
            {
                Item item = ItemModel.GetItem(prevTransform.name);
                this.CreatNewItem(item, enterTransform);
            }
            else//交换
            {
                //删除原来的物品
                Destroy(enterTransform.GetChild(0).gameObject);
                //获取数据
                Item prevGridItem = ItemModel.GetItem(prevTransform.name);
                Item enterGridItem = ItemModel.GetItem(enterTransform.name);
                //交换的两个物品
                this.CreatNewItem(prevGridItem, enterTransform);
                this.CreatNewItem(enterGridItem, prevTransform);
            }
        }
        else//拖到UI的其他地方
        {
            Item item = ItemModel.GetItem(prevTransform.name);
            this.CreatNewItem(item, prevTransform);
        }
    }
    #endregion
    /// <summary>
    /// 将物品信息排版 及 修改颜色 并返回该信息
    /// </summary>
    private string GetTooltipText(Item item)
    {
        if (item == null)
            return "";
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("<color=red>{0}</color>\n\n", item.Name);
        switch (item.ItemType)
        { 
            case "Armor":
                Armor armor = item as Armor;
                sb.AppendFormat("力量:{0}\n防御:{1}\n敏捷:{2}\n\n", armor.Power, armor.Defend, armor.Agility);
                break;
            case "Consumable":
                Consumable consumable = item as Consumable;
                sb.AppendFormat("HP:{0}\nMP:{1}\n\n", consumable.BackHp, consumable.BackMp);
                break;
            case "Weapon":
                Weapon weapon = item as Weapon;
                sb.AppendFormat("攻击:{0}\n\n", weapon.Damage);
                break;
            default:
                break;
        }
        sb.AppendFormat("<size=25><color=white>购买价格:{0}\n出售价格:{1}</color></size>\n\n<color=yellow><size=20>描述:{2}</size></color>", item.BuyPrice, item.SellPrice, item.Description);
        return sb.ToString();
    }
    /// <summary>
    /// 实例化物品 并 存储信息
    /// </summary>
    private void CreatNewItem(Item item, Transform parent)
    {
        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Item"); //加载资源[Item]
        itemPrefab.GetComponent<ItemUI>().UpdateItem(item.Name);

        GameObject itemGo = GameObject.Instantiate(itemPrefab); //实例化item
        itemGo.transform.SetParent(parent);

        itemGo.transform.localPosition = Vector3.zero; //重置位置信息
        itemGo.transform.localScale = Vector3.one;

        ItemModel.StoreItem(parent.name, item); //存储物品信息
    }
}
