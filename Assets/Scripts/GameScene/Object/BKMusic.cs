using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;

    public static BKMusic Instance => instance;

    public AudioSource bkSource;

    private void Awake()
    {
        instance = this;

        bkSource = this.GetComponent<AudioSource>();

        MusicInfo musicInfo = DataManager.Instance.musicInfo;
        SetMusicOpen(musicInfo.isOpen);
        SetMusicValue(musicInfo.musicValue);
        //GetComponent<AudioSource>().Play();
    }

    public void SetMusicOpen(bool open)
    {
        bkSource.mute = !open;
    }

    public void SetMusicValue(float value)
    {
        bkSource.volume = value;
    }
}
