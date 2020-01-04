using UnityEngine;
using System.Collections;
using System.Threading;

public class KnapsackManager : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform[] tranList;
    public ItemModel itemModel;
    public TipPanelUI tipPanel;
    public DragItemUI dragItem;

    private bool isShowTip = false;
    private bool isDragItem = false;

    void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, Input.mousePosition, null, out position);
        if (isDragItem)
        {
            dragItem.SetLocalPosition(position);
        }
        else if (isShowTip)
        {
            tipPanel.SetLocalPosition(position);
        }

        if (Input.GetKeyDown(KeyCode.E))
            CreateNewItem();
    }

    void Awake()
    {
        GridUI.OnEnter += GridUI_OnEnter;
        GridUI.OnExit += GridUI_OnExit;

        GridUI.OnBeginDrag += GridUI_OnBeginDrag;
        GridUI.OnEndDrag += GridUI_OnEndDrag;
    }

    private void GridUI_OnEnter(Transform gridTran) //显示信息
    {
        if (gridTran.childCount == 0 || isDragItem) return;
        ItemUI itemUI = gridTran.GetChild(0).GetComponent<ItemUI>();
        Item item = itemModel.itemListInfo[itemUI.Name.text];
        string des = item.Des;

        tipPanel.Show();
        tipPanel.SetTipContent(des);
        isShowTip = true;
    }

    private void GridUI_OnExit()
    {
        tipPanel.Hide();
        isShowTip = false;
    }

    private void GridUI_OnBeginDrag(Transform gridTran)   //拖拽
    {
        string itemName = gridTran.GetChild(0).GetComponent<ItemUI>().Name.text;
        //删除原有物品
        Destroy(gridTran.GetChild(0).gameObject);
        dragItem.SetDragItemName(itemName);
        dragItem.Show();
        isDragItem = true;
    }

    private void GridUI_OnEndDrag(Transform prevTran, Transform gridTran)
    {
        //分5种情况
        if(gridTran != null)//trans 不为空
        {
            if (gridTran.tag == "Grid")    //Grid
            {
                if (gridTran.childCount == 0)
                {
                    //在当前格子实例化物品
                    CreatItem(dragItem.text.text, gridTran);
                }
                else    //交换物品位置
                {
                    //存储当前格子物品
                    string gridItemName = gridTran.GetChild(0).GetComponent<ItemUI>().Name.text;
                    //将原有物品放下
                    gridTran.GetChild(0).GetComponent<ItemUI>().Name.text = dragItem.text.text;
                    //在原有位置存储当前物品
                    CreatItem(gridItemName, prevTran);
                }
            }
            else    //格子缝隙
	        {
                CreatItem(dragItem.text.text, prevTran);
	        }

            dragItem.Hide();
        }
        else    //空
        {
            string des = "物品已扔";
            dragItem.SetDragItemName(des);
            Invoke("ThrowItemTip", 0.3f);
        }

        
        isDragItem = false;
    }

    private void ThrowItemTip()
    {
        dragItem.Hide();
    }

    private void CreateNewItem()
    {
        int id = Random.Range(0, 10);
        Item item = itemModel.itemList[id];

        Transform emptyGrid = null;
            for (int i = 0; i < tranList.Length; i++)
                if (tranList[i].childCount == 0)
                {
                    emptyGrid = tranList[i];
                    CreatItem(item.Name, emptyGrid);

                    return;
                }
            Debug.LogWarning("背包已满！！");
    }

    private void CreatItem(string name, Transform parent)
    {
        GameObject itemGo = GameObject.Instantiate(itemPrefab);
        itemGo.GetComponent<ItemUI>().Name.text = name;
        itemGo.transform.SetParent(parent);
        itemGo.transform.localPosition = Vector3.zero;
        itemGo.transform.localScale = Vector3.one;
    }
}
