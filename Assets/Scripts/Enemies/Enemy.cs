using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids2;
using UnityEngine;

//This abstract class defines the common properties and methods for all types of enemies in the game. 
public abstract class Enemy : MonoBehaviour
{
    public Health Health { get; set; } // Property to access the enemy's health
    public abstract int ScoreValue { get; } // Abstract property to get the enemy's score value

    public abstract void SetTarget(); // Abstract method to set the enemy's target (e.g., player)

    public abstract void TakeDamage(int damageAmount); // Abstract method to handle enemy taking damage

    // Common method for handling enemy death
    protected void Die()
    {
        gameObject.SetActive(false); // Destroy the enemy game object
        EnemyManager.IsEnemyOnScene(false); // Notify the EnemyManager that the enemy is no longer on the scene
    }
}