using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource audioSource;
    [Space(10)]
    public AudioClip menuMusic;
    [Space(5)]
    public AudioClip gameMusic;

    [HideInInspector]
    public bool isMenuMusic;

    private static Music instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
            return;
        }

        if(!PlayerPrefs.HasKey("FirstEnterance")) 
        {
            isMenuMusic = true;
        }
    }

    public static void PlayMenuMusic()
    {
        if (instance.isMenuMusic)
        {
            return;
        }

        instance.audioSource.Stop();
        instance.audioSource.clip = instance.menuMusic;
        instance.audioSource.Play();
        instance.isMenuMusic = true;
    }

    public static void PlayGameMusic() 
    {
        if (!instance.isMenuMusic)
        {
            return;
        }

        instance.audioSource.Stop();
        instance.audioSource.clip = instance.gameMusic;
        instance.audioSource.Play();
        instance.isMenuMusic = false;
    }

    public static void Pause() => instance.audioSource.Stop();
    public static void Play() => instance.audioSource.Play();
}
