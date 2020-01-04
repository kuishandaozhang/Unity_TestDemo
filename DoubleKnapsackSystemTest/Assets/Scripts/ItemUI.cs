using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Text Name;

    public void SetItemName(string name)
    {
        this.Name.text = name;
    }
}
