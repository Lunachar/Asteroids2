using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    private static MusicManagerScript _instance;

    public static MusicManagerScript Instance
    {
        get { return _instance; }
    }

    internal AudioSource AudioSource;

    public AudioClip mainMenuMusic;
    public AudioClip gameMusic;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;

        DontDestroyOnLoad(this.gameObject);
        AudioSource = GetComponent<AudioSource>();
        Debug.Log($"audioSource1 is: {AudioSource.name}");
    }

    public void PlayMainMenuMusic()
    {
        AudioSource.clip = mainMenuMusic;
        Debug.Log($"audioSource2 is: {AudioSource.name}");
        AudioSource.loop = true;
        AudioSource.PlayOneShot(mainMenuMusic, 1f);
    }

    public void PlayGameMusic()
    {
        AudioSource.clip = gameMusic;
        Debug.Log($"audioSource3 is: {AudioSource.name}");
        AudioSource.loop = true;
        AudioSource.Play();
    }
}
