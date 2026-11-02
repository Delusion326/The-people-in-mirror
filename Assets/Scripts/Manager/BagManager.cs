using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager
{
    private static BagManager instance = new BagManager();

    public static BagManager Instance => instance;

    private BagManager()
    {
        for(int i = 0; i < DataManager.Instance.itemInfoList.Count; i++)
        {
            itemDic.Add(DataManager.Instance.itemInfoList[i].id, DataManager.Instance.itemInfoList[i]);
        }
    }

    private Dictionary<int , ItemInfo> itemDic = new Dictionary<int , ItemInfo>();

    /// <summary>
    /// 根据道具ID得到道具的详细信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ItemInfo GetItemInfo(int id)
    {
        if (itemDic.ContainsKey(id))
            return itemDic[id];
        return null;
    }


}
