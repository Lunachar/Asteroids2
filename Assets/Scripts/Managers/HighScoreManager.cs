using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Asteroids2;

public class HighScoreManager : MonoBehaviour
{
    private const String highScoreFileName = "highscore.json";
    
    public List<HighScoreEntry> highScores;

    [Serializable]
    public class HighScoreEntry
    {
        public string playerName;
        public float gameTime;
    }

    private void Awake()
    {
        LoadHighScores();
    }

    public bool IsHighScore(float gameTime)
    {
        bool isHighScore = false;
        foreach (var entry in highScores)
        {
            Debug.LogError($"IsHighScore: {entry.gameTime} :: {highScores.Capacity}");
            if (gameTime < entry.gameTime || highScores.Capacity < 10)
            {
                isHighScore = true;
                break;
            }
        }
        return isHighScore;
    }

    public void AddHighScore(string playerName, float gameTime)
    {
        HighScoreEntry newEntry = new HighScoreEntry {playerName = playerName, gameTime = gameTime};
        highScores.Add(newEntry);
        
        highScores.Sort((a, b) => a.gameTime.CompareTo(b.gameTime));

        int maxHighScores = 10;
        if (highScores.Count > maxHighScores)
        {
            highScores.RemoveRange(maxHighScores, highScores.Count - maxHighScores);
        }

        SaveHighScores();
    }

    internal void LoadHighScores()
    {
        string filePath = Path.Combine(Application.persistentDataPath, highScoreFileName);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            highScores = JsonUtility.FromJson<HighScoreData>(json).entries;
        }
        else
        {
            highScores = new List<HighScoreEntry>();
        }
    }

    private void SaveHighScores()
    {
        {
            HighScoreData data = new HighScoreData {entries = highScores};
            string json = JsonUtility.ToJson(data);
            string filePath = Path.Combine(Application.persistentDataPath, highScoreFileName);
            File.WriteAllText(filePath, json);
            
        }
    }
    
    [Serializable]
    public class HighScoreData
    {
        public List<HighScoreEntry> entries;
    }
}
