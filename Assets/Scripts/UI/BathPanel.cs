using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BathPanel : BasePanel
{
    public Image water;

    public Button btnWoodCork;

    public Button btnExit;

    public bool isPress;

    public float speed;

    public float nowHeight;



    public override void Init()
    {
        btnWoodCork.onClick.AddListener(() =>
        {
            isPress = !isPress;
            speed = 10f;

            if(nowHeight <=225 && nowHeight >= 220)
            {
                //通关
                UIManager.Instance.HideMe<BathPanel>();
                GameObject.Find("浴缸").GetComponent<PolygonCollider2D>().enabled = false;
                GameObject calendar = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/ItemObj/canlder_3"));
                UIManager.Instance.ShowMe<TipPanel>().ShowTip("水位刚好，怪物无法行动，任务完成");
                calendar.transform.SetParent(GameObject.Find("canlder_3Pos").transform,false);
            }

            if(nowHeight <= 220)
            {
                //失败
                GameLevelMgr.Instance.GameOver("触发梦中梦结局");
                UIManager.Instance.HideMe<BathPanel>();
                UIManager.Instance.ShowMe<TipPanel>().ShowTip("水位过低，怪物被释放，任务失败");
            }
        });

        btnExit.onClick.AddListener(() =>
        {
            UIManager.Instance.HideMe<BathPanel>();
        });
    }

    protected override void Update()
    {
        base.Update();
        if (isPress)
        {
            nowHeight = water.gameObject.GetComponent<RectTransform>().sizeDelta.y;
            print(nowHeight);
            water.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(water.gameObject.GetComponent<RectTransform>().sizeDelta.x
                              , nowHeight - speed * Time.deltaTime);
        }
       
    }

 
}
