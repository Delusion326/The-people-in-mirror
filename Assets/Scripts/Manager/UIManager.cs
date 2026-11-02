using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class UIManager
{
    private static UIManager instance = new UIManager();

    public static UIManager Instance => instance;

    //用于后续动态创建面板时 设置父对象
    public Transform canvasTrans;

    //用于存储每一个显示着的面板
    //隐藏面板时 直接获得字典中对应的面板 进行隐藏
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    private UIManager()
    {
        //得到场景中的canvas
        GameObject canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        canvasTrans = canvas.transform;
        //通过过场景不移除对象 保证对象唯一
        GameObject.DontDestroyOnLoad(canvas);
    }

    /// <summary>
    /// 显示面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public T ShowMe<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;

        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;

        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        panelObj.transform.SetParent(canvasTrans, false);

        T panel = panelObj.GetComponent<T>();

        panelDic.Add(panelName, panel);

        panel.ShowMe();

        return panel as T;

        

    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void HideMe<T>(bool isFade = true) where T : BasePanel
    {
        string panelName = typeof(T).Name;

        if (panelDic.ContainsKey(panelName))
        {   
            if(isFade)
            {
                //如果要淡入淡出效果 则给面板的隐藏函数添加事件
                panelDic[panelName].HideMe(() =>
                {
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
            
        }
            

        
        
    }

    public void HideAllPanel()
    {
        foreach (var item in panelDic)
        {
            GameObject.Destroy(item.Value.gameObject);
        }
        panelDic.Clear();
    }
    /// <summary>
    /// 得到面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T ).Name;

        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;

        return null;
    }

    //判断是否存在除基本游戏面板以外的其他面板
    public bool IsExitPanel()
    {
        if(panelDic.Count >= 2)
        {
            return true;
        }

        return false;
    }

}
