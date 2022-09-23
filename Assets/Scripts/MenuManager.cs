using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public string currentPlayer;
    public int currentScore;

    public string bestPlayer;
    public int bestScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //ResetGame();
    }

    [System.Serializable]
    class SaveData
    {
        public string topPlayer;
        public int topScore;
    }

    public void SaveScore()
    {
        if (currentScore > bestScore)
        {
            SaveData data = new SaveData();
            data.topPlayer = currentPlayer;
            data.topScore = currentScore;
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestPlayer = data.topPlayer;
            bestScore = data.topScore;
        }
    }

    public void ResetGame()
    {
        SaveData data = new SaveData();
        data.topPlayer = "";
        data.topScore = 0;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
}
