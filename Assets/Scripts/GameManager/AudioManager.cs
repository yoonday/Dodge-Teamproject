using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    
    [Header("BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("#SFX")]
    public AudioClip[] sfxClip;
    public float sfxVolume;
    public int Channels;
    AudioSource[] sfxPlayer;
    int ChannelIndex;


    void init()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        
    
    }


}
