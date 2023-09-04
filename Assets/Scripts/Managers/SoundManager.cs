using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip screenEdgeCollisionSound;

    private void Awake()
    {
        _audioSource = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PlayerModel.OnScreenEdgeCollision += PlayScreenEdgeCollisionSound;
    }
    
    private void OnDisable()
    {
        PlayerModel.OnScreenEdgeCollision -= PlayScreenEdgeCollisionSound;
    }

    private void PlayScreenEdgeCollisionSound()
    {
        _audioSource.PlayOneShot(screenEdgeCollisionSound);
    }
}
