using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private static DataManager instance = new DataManager();

    public static DataManager Instance => instance;

    //所有的道具信息
    public List<ItemInfo> itemInfoList;

    //玩家拥有的道具信息
    public List<Items> playerItem;

    //音乐数据
    public MusicInfo musicInfo;
 
    private DataManager()
    {
        //加载所有道具信息
        itemInfoList = JsonMgr.Instance.LoadData<List<ItemInfo>>("ItemInfo");

        //加载玩家拥有的道具信息
        playerItem = JsonMgr.Instance.LoadData<List<Items>>("PlayerItem");

        //加载音乐数据
        musicInfo = JsonMgr.Instance.LoadData<MusicInfo>("MusicInfo");
    }
    /// <summary>
    /// 保存玩家拥有的道具信息
    /// </summary>
    public void SavePlayerItemData()
    {
        JsonMgr.Instance.SaveData(playerItem, "PlayerItem");
        Debug.Log(Application.persistentDataPath + "/PlayerItem.json");
    }

    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicInfo, "MusicInfo");
    }

    //public void PlaySound(string resName)
    //{
    //    GameObject musicObj = new GameObject();
    //    AudioSource audio = musicObj.AddComponent<AudioSource>();
    //    audio.clip = Resources.Load<AudioClip>(resName);
    //    audio.volume = musicInfo.musicValue;
    //    audio.mute = !musicInfo.isOpen;
    //    audio.Play();

    //    //GameObject.Destroy(musicObj, 1);
    //}
}
