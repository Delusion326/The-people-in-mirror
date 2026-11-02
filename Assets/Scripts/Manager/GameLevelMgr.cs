using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevelMgr
{

    private static GameLevelMgr instance = new GameLevelMgr();

    public static GameLevelMgr Instance => instance;

    //玩家拥有的道具
    public List<Items> playerItems;

    private GameLevelMgr()
    {
        playerItems = DataManager.Instance.playerItem;
    }

    //游戏是否结束
    public bool isGameOver;

    //存档点
    public int savePoint;

    //玩家正在使用的道具
    public int playerUsingItemID;


    public void AddPlayerItem(int id)
    {
        Items items = new Items();
        items.id = id;
        if (playerItems == null)
        {
            playerItems = new List<Items>();
        }
        playerItems.Add(items);
        Debug.Log("playerItems : " + playerItems.Count);
        //保存玩家道具数据
        DataManager.Instance.playerItem = playerItems;
        DataManager.Instance.SavePlayerItemData();
    }

    public void RemovePlayerItem(int id)
    {
        if (playerItems == null)
        {
            return;
        }
        playerItems.RemoveAll(item => item.id == id);
        Debug.Log("playerItems : " + playerItems.Count);
        //保存玩家道具数据
        DataManager.Instance.playerItem = playerItems;
        DataManager.Instance.SavePlayerItemData();
    }

    public void LoadNextLevel()
    {
        //加载下一关的逻辑
        //如果死亡 则返回上个存档点
        AsyncOperation ao;
        string sceneName = "BeginScene";
        switch (savePoint)
        {
            case 1:
                sceneName = "GameScene_1_livingRoom";
                break;
            case 2:
                sceneName = "GameScene_3_kitchen";
                break;
            case 3:
                sceneName = "GameScene_2_bathRoom";
                break;
            case 4:
                sceneName = "GameScene_4_bedRoom";
                break;
        }

        if (isGameOver)
        {
                ao = SceneManager.LoadSceneAsync(sceneName);
        }
        else
        {
             ao = SceneManager.LoadSceneAsync(sceneName);
        }
        GameObject.Destroy(PlayerObject.Instance.gameObject);
        
        ao.completed += (AsyncOperation obj) =>
        {
            //加载完成后执行的逻辑
            //初始化玩家位置
            GameObject player = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Player/Player"));
            GameObject born = GameObject.Find("BornPos");
            CinemachineVirtualCamera vc = GameObject.Find("Virtual Camera")?.GetComponent<CinemachineVirtualCamera>();
            if (vc != null)
            {
                vc.Follow = player.transform;
            }
            if (born != null)
                player.transform.SetParent(born.transform, false);
        };
        
    }

    public void GameOver(string text)
    {
        //UIManager.Instance.HideAllPanel();
        UIManager.Instance.ShowMe<GameOverPanel>().SetTip(text);
        isGameOver = true;
        GameObject.Find("BKMusic").GetComponent<AudioSource>().Stop();
        //GameObject.Find("DreamMusic").GetComponent<AudioSource>().Play();
        GameObject bkMusic = new GameObject();
        AudioSource audioSource = bkMusic.AddComponent<AudioSource>();
        switch (text)
        {
            case "触发梦中梦结局":    
                audioSource.clip = Resources.Load<AudioClip>("Audio/DreamMusic");
                break;
            case "触发真结局":
                audioSource.clip = Resources.Load<AudioClip>("Audio/TrueEndingMusic");
                break;
        }
        PlayerObject.Instance.isMove = false;
    }

}
