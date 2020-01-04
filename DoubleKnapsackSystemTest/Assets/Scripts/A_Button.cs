using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class A_Button : MonoBehaviour
{
    public GameObject Knapsack;
    private bool isShowKnapsack = false;

    public void OnButtonClick()
    {
        isShowKnapsack = !isShowKnapsack;
        if (isShowKnapsack)
            Knapsack.gameObject.SetActive(true);
        else
            Knapsack.gameObject.SetActive(false);
    }
}
