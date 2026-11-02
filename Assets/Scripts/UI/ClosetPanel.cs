using System.Collections.Generic;
using UnityEngine;

public class ClosetPanel : BasePanel
{
    public List<Items> playerHaveItem;

    public override void Init()
    {
        playerHaveItem = DataManager.Instance.playerItem;  
    }

    public void AddGameItem()
    {
        for (int i = 0; i < DataManager.Instance.playerItem.Count; i++)
        {
            if (DataManager.Instance.playerItem[i].id == 9 || DataManager.Instance.playerItem[i].id == 6)
            {
                return;
            }
        }
        GameObject hammer = new GameObject();
        ItemObject hammerItem = hammer.AddComponent<ItemObject>();
        hammerItem.keyItemObject = Resources.Load<KeyItemObject>("KeyItemInfo/ItemObject/hammer_hand");
        UIManager.Instance.ShowMe<ShowItemPanel>().SetObject(hammer.transform);
        UIManager.Instance.GetPanel<ShowItemPanel>().nowSelSprite.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 50);
        int itemID = hammer.GetComponent<ItemObject>().keyItemObject.id;
        GameLevelMgr.Instance.AddPlayerItem(itemID);
        UIManager.Instance.GetPanel<TipPanel>().btnExit.onClick.RemoveListener(AddGameItem);

        for(int i = 0;i < DataManager.Instance.playerItem.Count;i++)
        {
            if(DataManager.Instance.playerItem[i].id == 8)
            {
                AddHammer();
                break;
            }
        }
        Destroy(hammer);
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
