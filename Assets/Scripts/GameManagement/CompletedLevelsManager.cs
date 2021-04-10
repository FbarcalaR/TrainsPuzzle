using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class CompletedLevelsManager : MonoBehaviour {
    private List<int> completedLevels;

    private string SaveFilePath => Application.persistentDataPath + "/gamesave.save";

    private void Awake() {
        LoadCompletedLevels();
    }

    public void AddCompletedLevel(int level) {
        completedLevels.Add(level);
        SaveCompletedLevels();
    }

    public bool IsLevelCompleted(int level) {
        return completedLevels.Contains(level);
    }

    public void SaveCompletedLevels() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(SaveFilePath);
        bf.Serialize(file, completedLevels);
        file.Close();
    }

    public void LoadCompletedLevels() {
        if (File.Exists(SaveFilePath)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(SaveFilePath, FileMode.Open);
            completedLevels = (List<int>) bf.Deserialize(file);
            file.Close();
            Debug.Log("Game Loaded");
        }
        else {
            completedLevels = new List<int>();
            Debug.Log("No game saved!");
        }
    }

}
