using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource audioSource;

    public bool bgmOn = true;

    public float defaultVolume = 0.1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        defaultVolume = audioSource.volume;
        if (!PrefManager.GetSound())
        {
            audioSource.volume = 0;
            bgmOn = false;
        } else
        {
            bgmOn = true;
        }
    }

    public void Play(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void SoundOff()
    {
        bgmOn = false;
        audioSource.volume = 0;
        PrefManager.SetSound(false);
    }

    public void SoundOn()
    {
        bgmOn = true;
        audioSource.volume = defaultVolume;
        PrefManager.SetSound(true);
    }
}
