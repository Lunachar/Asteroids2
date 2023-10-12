using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    private const string highScoreFileName = "highscore.json";
    
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
        //Debug.LogError($"{highScoreFileName}, {highScores.Capacity.ToString()}, {highScores.FindLastIndex(null!).ToString()}");
        foreach (var entry in highScores)
        {
            Debug.LogError($"IsHighScore: {entry.gameTime} :: {highScores.Capacity}");
            if (gameTime < entry.gameTime || highScores.Capacity < 10)
            {
                return true;
            }
        }
        return false;
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
        Debug.Log($"DataPath: {Application.persistentDataPath}");
        Debug.Log($"FileName: {highScoreFileName}");
        string filePath = Path.Combine(Application.persistentDataPath, highScoreFileName);
        Debug.LogError($"FullPath: {filePath}");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            Debug.LogError($"{json}");
            highScores = JsonUtility.FromJson<HighScoreData>(json).entries;
            Debug.LogError($"{highScores.Capacity}, {highScores.Count}");
        }
        else
        {
            highScores = new List<HighScoreEntry>();
            Debug.LogError($"{highScores.Capacity}");
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
