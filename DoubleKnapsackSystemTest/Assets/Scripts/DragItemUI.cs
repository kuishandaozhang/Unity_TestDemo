using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DragItemUI : MonoBehaviour
{
    public Text text;

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

    public void SetDragItemName(string name)
    {
        text.text = name;
    }
}
