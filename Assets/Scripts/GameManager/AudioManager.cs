using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("BGM")]
    public AudioClip BgmClip;
    public float BgmVolume;
    AudioSource BgmPlayer;

    [Header("#SFX")]
    public AudioClip[] sfxClip;
    public float sfxVolume;
    public int Channels;
    AudioSource[] sfxPlayer;
    int ChannelIndex;

    private void Awake()
    {
        
        Instance = this;
        Init();
    }

    void Init()
    { 
    
        
    
    
    }


}
