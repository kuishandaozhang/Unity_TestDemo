using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TipPanelUI : MonoBehaviour
{
    public Text outline;//控制大小
    public Text content;//显示内容
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    public void SetTipContent(string des)
    {
        outline.text = des;
        content.text = des;
    }
}
