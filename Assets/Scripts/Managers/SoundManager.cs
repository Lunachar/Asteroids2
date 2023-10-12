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
    public AudioClip soundPlayerCollide;
    
    private bool _isEdgeCollisionSoundPlaying;
    private bool _isPlayerCollidePlaying;

    private void Start()
    {
        _audioSource = MusicManagerScript.Instance.AudioSource;
    }

    private void OnEnable()
    {
        PlayerModel.OnScreenEdgeCollision += PlayScreenEdgeCollisionSound;
        PlayerModel.PlayerGunShoot += PlayPlayerGunShoot;
        PlayerModel.PlayerCollide += PlayPlayerCollide;
    }
    
    private void OnDisable()
    {
        PlayerModel.OnScreenEdgeCollision -= PlayScreenEdgeCollisionSound;
        PlayerModel.PlayerGunShoot -= PlayPlayerGunShoot;
        PlayerModel.PlayerCollide -= PlayPlayerCollide;
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

    private void PlayPlayerCollide()
    {
        if (_isPlayerCollidePlaying) return;
        _audioSource.PlayOneShot(soundPlayerCollide);
        _isPlayerCollidePlaying = true;
        StartCoroutine(WaitForPlayerCollideSound());
    }

    private IEnumerator WaitForEdgeCollisionSound()
    {
        yield return new WaitForSeconds(soundPlayerEdgeCollision.length);
        _isEdgeCollisionSoundPlaying = false;
    }

    private IEnumerator WaitForPlayerCollideSound()
    {
        yield return new WaitForSeconds(soundPlayerCollide.length);
        _isPlayerCollidePlaying = false;
    }
}
