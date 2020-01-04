using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour
{
    public Text OutlineText; //控制提示信息卡的大小
    public Text ContentText; //显示物品信息
    /// <summary>
    /// 更新信息卡内容
    /// </summary>
    public void UpdateTooltip(string text)
    {
        OutlineText.text = text;
        ContentText.text = text;
    }
    /// <summary>
    /// 显示信息卡
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }
    /// <summary>
    /// 隐藏信息卡
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 设置信息卡跟随鼠标
    /// </summary>
    public void SetLocalPosition(Vector2 position)
    {
        transform.localPosition = position;
    }
}
