using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids2;
using Player;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;

    public AudioClip _sound_PlayerEdgeCollision;
    public AudioClip _sound_PlayerGunShoot;
    
    private bool _isEdgeCollisionSoundPlaying = false;

    private void Start()
    {
        _audioSource = MusicManagerScript.Instance._audioSource;
    }

    private void OnEnable()
    {
        PlayerModel.OnScreenEdgeCollision += PlayScreenEdgeCollisionSound;
        PlayerModel.PlayerGunShoot += PlayPlayerGunShoot;
    }
    
    private void OnDisable()
    {
        PlayerModel.OnScreenEdgeCollision -= PlayScreenEdgeCollisionSound;
        PlayerModel.PlayerGunShoot -= PlayPlayerGunShoot;
    }

    private void PlayScreenEdgeCollisionSound()
    {
        if (_isEdgeCollisionSoundPlaying) return;
        _audioSource.PlayOneShot(_sound_PlayerEdgeCollision, 0.4f);
        _isEdgeCollisionSoundPlaying = true;
        StartCoroutine(WaitForEdgeCollisionSound());
    }

    private void PlayPlayerGunShoot()
    {
        _audioSource.PlayOneShot(_sound_PlayerGunShoot, 0.2f);
    }

    private IEnumerator WaitForEdgeCollisionSound()
    {
        yield return new WaitForSeconds(_sound_PlayerEdgeCollision.length);
        _isEdgeCollisionSoundPlaying = false;
    }
}
