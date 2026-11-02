using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Button btnBag;

    public override void Init()
    {
        btnBag.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowMe<BagPanel>();
        });
    }

    public override void ShowMe()
    {
        base.ShowMe();
        PlayerObject.Instance.isMove = true;
    }
}
