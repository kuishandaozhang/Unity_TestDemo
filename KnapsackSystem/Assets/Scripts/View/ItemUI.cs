using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {

    public Text ItemName;

    public void UpdateItem(string name)
    {
        this.ItemName.text = name;
    }
}
