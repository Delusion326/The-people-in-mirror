using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有道具的基本信息
/// </summary>
[System.Serializable]
public class ItemInfo
{
    public int id;
    public string name;
    public string icon;
    public string info;
    public bool isScene;
    public string toScene;
}

/// <summary>
/// 玩家拥有的道具信息
/// </summary>
[System.Serializable]
public class Items
{
    public int id;
}