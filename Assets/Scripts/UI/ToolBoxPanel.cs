using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBoxPanel : ShowItemPanel
{

    public InputField inputPassword;

    public List<Items> playerHaveItem;

    public override void Init()
    {
        base.Init();

        playerHaveItem = DataManager.Instance.playerItem;

        exitBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.HideMe<ToolBoxPanel>();
        });

        inputPassword.onValueChanged.AddListener((string password) =>
        {
            if (password == "0412")
            {
                for (int i = 0; i < DataManager.Instance.playerItem.Count; i++)
                {
                    if (DataManager.Instance.playerItem[i].id == 8 || DataManager.Instance.playerItem[i].id == 6)
                    {
                        return;
                    }
                }

                GameObject hammer = new GameObject();
                ItemObject hammerItem = hammer.AddComponent<ItemObject>();
                hammerItem.keyItemObject = Resources.Load<KeyItemObject>("KeyItemInfo/ItemObject/hammer_head");
                UIManager.Instance.ShowMe<ShowItemPanel>().SetObject(hammer.transform);
                UIManager.Instance.GetPanel<ShowItemPanel>().nowSelSprite.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 50);
                int itemID = hammer.GetComponent<ItemObject>().keyItemObject.id;
                GameLevelMgr.Instance.AddPlayerItem(itemID);
                AddCalendar();
                Destroy(hammer);

                UIManager.Instance.HideMe<ToolBoxPanel>();
                Destroy(GameObject.Find("toolBox"));
            }
        });

    }

    public void AddCalendar()
    {
        print("add calendar");
        GameObject calendar = new GameObject();
        ItemObject calendarItem = calendar.AddComponent<ItemObject>();
        calendarItem.keyItemObject = Resources.Load<KeyItemObject>("KeyItemInfo/ItemObject/calendar_4");
        UIManager.Instance.ShowMe<ShowItemPanel>().SetObject(calendar.transform);
        int itemID = calendar.GetComponent<ItemObject>().keyItemObject.id;
        GameLevelMgr.Instance.AddPlayerItem(itemID);

        //如果同时拥有锤柄和锤头，则将锤头替换为锤子
        for (int i = 0; i < DataManager.Instance.playerItem.Count; i++)
        {
            if (DataManager.Instance.playerItem[i].id == 9)
            {
                AddHammer();
                break;
            }
        }
        Destroy(calendar);
    }

    public void AddHammer()
    {
        GameObject hammer = new GameObject();
        ItemObject hammerItem = hammer.AddComponent<ItemObject>();
        hammerItem.keyItemObject = Resources.Load<KeyItemObject>("KeyItemInfo/ItemObject/hammer");
        UIManager.Instance.ShowMe<ShowItemPanel>().SetObject(hammer.transform);
        UIManager.Instance.GetPanel<ShowItemPanel>().nowSelSprite.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 50);
        int itemID = hammer.GetComponent<ItemObject>().keyItemObject.id;
        GameLevelMgr.Instance.AddPlayerItem(itemID);
        GameLevelMgr.Instance.RemovePlayerItem(8);
        GameLevelMgr.Instance.RemovePlayerItem(9);
        Destroy(hammer);

    }

}
