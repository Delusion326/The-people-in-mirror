using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Button btnExit;

    public Toggle toggleMusic;

    public Slider sliderMusic;

    public override void Init()
    {
        MusicInfo musicInfo = DataManager.Instance.musicInfo;
        toggleMusic.isOn = musicInfo.isOpen;
        sliderMusic.value = musicInfo.musicValue;

        btnExit.onClick.AddListener(() =>
        {
            UIManager.Instance.HideMe<SettingPanel>();
            DataManager.Instance.SaveMusicData();
        });

        toggleMusic.onValueChanged.AddListener((bool isOn) =>
        {
            //调整音乐开关
            BKMusic.Instance.SetMusicOpen(isOn);
            DataManager.Instance.musicInfo.isOpen = isOn;
        });

        sliderMusic.onValueChanged.AddListener((float value) =>
        {
            //调整音乐音量
            BKMusic.Instance.SetMusicValue(value);
            DataManager.Instance.musicInfo.musicValue = value;
        });
    }
}
