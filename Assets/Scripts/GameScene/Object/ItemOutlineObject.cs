using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOutlineObject : MonoBehaviour
{
    public GameObject outlineObject;

    //当人物靠近时显示轮廓
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("碰撞了");
        if (collision.CompareTag("Player"))
        {
            outlineObject.SetActive(true);
        }
    }

    //当人物离开时隐藏轮廓
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            outlineObject.SetActive(false);
        }
    }
}
