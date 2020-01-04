using UnityEngine;
using System.Collections;
/// <summary>
/// 手动添加物品[测试]
/// </summary>
public class InputDetector : MonoBehaviour
{

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(2))
        {
            int index = Random.Range(0, 10);
            KnapsackManager.Instance.StoreItem(index);
        }
	}
}
