using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {
  public static GameManager Instance;
  public int CurrentScore;

  private void Awake() {
    if (Instance == null) {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    } else {
      Destroy(gameObject);
    }
  }

  [Serializable]
  class SaveData {
    public List<GameScore> Scores = new List<GameScore>();
  }

  [Serializable]
  public class GameScore {
    public string Name;
    public int Score;

    public GameScore(string name, int score) {
      Name = name;
      Score = score;
    }
  }

  public static void SaveScore(GameScore score) {
    SaveData loadedSaveData = LoadSaveData();
    loadedSaveData.Scores.Add(score);
    string json = JsonUtility.ToJson(loadedSaveData);
    File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
  }

  public static GameScore LoadHighScore() {
    SaveData loadedSaveData = LoadSaveData();
    List<GameScore> sortedScores = loadedSaveData.Scores.OrderByDescending(s => s.Score).ToList();
    if (sortedScores.Count > 0) {
      return sortedScores[0];
    }
    return null;
  }

  private static SaveData LoadSaveData() {
    string path = Application.persistentDataPath + "/savefile.json";
    if (File.Exists(path)) {
      string json = File.ReadAllText(path);
      return JsonUtility.FromJson<SaveData>(json);
    }
    return new SaveData();
  }
}
