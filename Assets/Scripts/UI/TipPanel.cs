using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    public Button btnExit;

    public Text txtTip;

    public override void Init()
    {
        btnExit.onClick.AddListener(() =>
        {
            UIManager.Instance.HideMe<TipPanel>();
            if(UIManager.Instance.GetPanel<ClosetPanel>())
                UIManager.Instance.HideMe<ClosetPanel>();
        }); 
    }

    public void ShowTip(string tip)
    {
        txtTip.text = tip;
    }


}
