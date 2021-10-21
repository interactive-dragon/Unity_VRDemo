using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;

public class Leaderboard 
{
    private string m_Filepath;

    [JsonProperty]
    public int Highscore { get; protected set; }

    [JsonIgnore]
    public Action<int> HighscoreChanged;

    public Leaderboard(string filePath)
    {
        m_Filepath = filePath;
        Load();
    }

    private void Load()
    {
        if (File.Exists(m_Filepath))
        {
            using (StreamReader file = File.OpenText(m_Filepath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Populate(file, this);
            }
        }
    }

    private void Save()
    {
        using (StreamWriter file = File.CreateText(m_Filepath))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, this);
        }
    }

    public void RegisterScore(int score)
    {
        if (score > Highscore)
        { 
            Highscore = score;
            if(HighscoreChanged != null)
                HighscoreChanged(Highscore);
            Save();
        } 
    }
}
