using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 废弃的存档点类，暂时没有用到
/// </summary>
public class SavePointObject : MonoBehaviour
{
    private static SavePointObject instance;

    public static SavePointObject Instance => instance;

    private SavePointObject() { }

    //private void Awake()
    //{
    //    instance = this;
    //}
    ////存档点ID
    //public int savePointID;


    //public void Save()
    //{
    //    GameLevelMgr.Instance.savePoint = savePointID;
    //    DataManager.Instance.SavePlayerItemData();

    //}

}
