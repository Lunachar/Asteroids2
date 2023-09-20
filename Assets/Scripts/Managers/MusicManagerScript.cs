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

    internal AudioSource _audioSource;

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
        _audioSource = GetComponent<AudioSource>();
        Debug.Log($"audioSource1 is: {_audioSource}");
    }

    public void PlayMainMenuMusic()
    {
        _audioSource.clip = mainMenuMusic;
        Debug.Log($"audioSource2 is: {_audioSource}");
        _audioSource.loop = true;
        _audioSource.PlayOneShot(mainMenuMusic, 1f);
    }

    public void PlayGameMusic()
    {
        _audioSource.clip = gameMusic;
        Debug.Log($"audioSource3 is: {_audioSource}");
        _audioSource.loop = true;
        _audioSource.Play();
    }
}
