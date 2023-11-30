using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids2;
using Player;
using UnityEngine;
using UnityEngine.Audio;


public class SetSpeed : MonoBehaviour
{
    public PlayerModel playerModel;
    public GameState gameState;

    private void Update()
    {
        if (playerModel == null)
        {
            SetPlayerModel();
        }
    }

    private void SetPlayerModel()
    {
        playerModel = GameObject.Find("Player").GetComponent<PlayerModel>();
    }

    public void SetValue(float sliderValue)
    {
        if (playerModel != null)
        {
            playerModel._speed = sliderValue;
        }
    }
}
