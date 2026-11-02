using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BagPanel : BasePanel
{
    public Button btnExit;

    public Button btnToScene;

    public Button btnUseItem;

    public Text txtItemInfo;

    public Text txtName;

    public Transform itemPos;

    public BagItemObject nowSelObject;

    //玩家拥有的道具信息
    private List<Items> itemsList;

    //所有的道具信息
    private List<ItemInfo> itemInfoList;

    public override void Init()
    {
        //获取玩家拥有的道具信息
        itemsList = DataManager.Instance.playerItem;
        //所有道具信息
        itemInfoList = DataManager.Instance.itemInfoList;
        if(itemsList != null && itemsList.Count>0)
        {
            //初始化界面显示第一个道具的信息
            for (int i = 0; i < itemInfoList.Count; i++)
            {
                if (itemsList[0].id == itemInfoList[i].id)
                {
                    if (itemInfoList[i].isScene)
                    {
                        btnToScene.gameObject.SetActive(true);
                        btnUseItem.gameObject.SetActive(false);
                    }
                    else
                    {
                        btnToScene.gameObject.SetActive(false);
                        btnUseItem.gameObject.SetActive(true);
                    }
                    txtItemInfo.text = itemInfoList[i].info;
                    txtName.text = itemInfoList[i].name;
                    break;
                }
            }
        }
        

        btnExit.onClick.AddListener(() =>
        {
            UIManager.Instance.HideMe<BagPanel>();
        });

        btnToScene.onClick.AddListener(() =>
        {
            UIManager.Instance.HideMe<BagPanel>();
            Destroy(PlayerObject.Instance.gameObject);
            AsyncOperation ao = SceneManager.LoadSceneAsync(nowSelObject.toSceneName);
            ao.completed += (AsyncOperation obj) =>
            {
                if(nowSelObject.toSceneName == "GameScene_1_livingRoom")
                {
                    GameLevelMgr.Instance.savePoint = 4;
                }
                else if (nowSelObject.toSceneName == "GameScene_2_bathRoom")
                {
                    GameLevelMgr.Instance.savePoint = 2;
                    UIManager.Instance.ShowMe<TipPanel>().ShowTip("“浴室的水太深了”");
                }
                else if (nowSelObject.toSceneName == "GameScene_3_kitchen")
                {
                    GameLevelMgr.Instance.savePoint = 1;
                }
                else if (nowSelObject.toSceneName == "GameScene_4_bedRoom")
                {
                    GameLevelMgr.Instance.savePoint = 3;
                }
                GameObject player = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Player/Player"));
                CinemachineVirtualCamera vc = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
                player.transform.SetParent(GameObject.Find("BornPos").transform, false);
                vc.Follow = player.transform;
            };
        });

        btnUseItem.onClick.AddListener(() =>
        {
            GameLevelMgr.Instance.playerUsingItemID = nowSelObject.itemID;
            string itemName = nowSelObject.itemName;
            print(nowSelObject.itemID);
            switch (itemName)
            {
                //使用手电筒
                case "手电筒":     
                    PlayerObject.Instance.gameObject.GetComponentInChildren<Light2D>().intensity = 1;
                    //Debug.Log(PlayerObject.Instance.usingItemID);
                    break;
                //使用锤子
                case "锤子":  
                    break;
                //使用木塞
                case "木塞":
                    
                    break;
            }
        });

        if(itemsList != null && itemsList.Count > 0)
            CreateItem();

    }

    public void CreateItem()
    {
        for (int i = 0; i < itemsList.Count; i++)
        {
            GameObject itemObj = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/ItemObj/Item"));
            itemObj.transform.SetParent(itemPos, false);
            BagItemObject bagItem = itemObj.transform.GetComponent<BagItemObject>();
            //itemObj.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("ItemIcon" + itemsList[i].id);
            for (int j = 0; j < itemInfoList.Count; j++)
            {
                //Debug.Log("itemsList[i].id : " + itemsList[i].id);
                //Debug.Log("itemInfoList[j].id : " + itemInfoList[j].id);
                if (itemsList[i].id == itemInfoList[j].id)
                {
                    itemObj.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("ItemIcon/" + itemInfoList[j].icon);


                    bagItem.itemName = itemInfoList[j].name;
                    bagItem.itemInfo = itemInfoList[j].info;
                    bagItem.isScene = itemInfoList[j].isScene;
                    bagItem.toSceneName = itemInfoList[j].toScene;
                    bagItem.itemID = itemInfoList[j].id;

                    if (itemInfoList[j].id == itemsList[0].id)
                    {
                        nowSelObject = bagItem;
                    }
                    //itemObj.transform.GetComponent<Button>().onClick.AddListener(() =>
                    //{
                    //    txtName.text = itemInfoList[j].name;
                    //    txtItemInfo.text = itemInfoList[j].info;

                    //});
                    break;
                }
            }
        }
    }
}
