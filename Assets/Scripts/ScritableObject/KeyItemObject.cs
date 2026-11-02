using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KeyItemObject : ScriptableObject
{
    //道具id
    public int id;
    //道具图片
    public Sprite sprite;
    //道具的作用信息
    [TextArea(4,20)]
    public string info;

    //是否需要跳转场景
    public bool isScene;
    //所要跳转的场景名字
    public string sceneName;

}
