using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids2;
using Player;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;

    public AudioClip soundPlayerEdgeCollision;
    public AudioClip soundPlayerGunShoot;
    
    private bool _isEdgeCollisionSoundPlaying = false;

    private void Start()
    {
        _audioSource = MusicManagerScript.Instance.AudioSource;
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
        _audioSource.PlayOneShot(soundPlayerEdgeCollision, 0.4f);
        _isEdgeCollisionSoundPlaying = true;
        StartCoroutine(WaitForEdgeCollisionSound());
    }

    private void PlayPlayerGunShoot()
    {
        _audioSource.PlayOneShot(soundPlayerGunShoot, 0.2f);
    }

    private IEnumerator WaitForEdgeCollisionSound()
    {
        yield return new WaitForSeconds(soundPlayerEdgeCollision.length);
        _isEdgeCollisionSoundPlaying = false;
    }
}
