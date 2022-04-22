using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip bgmMusic;

    void Start()
    {
        AudioManager.Instance.Play(bgmMusic);
    }

}
