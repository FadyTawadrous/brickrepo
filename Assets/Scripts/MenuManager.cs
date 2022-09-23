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

    public TextMeshProUGUI inputField;
    public TextMeshProUGUI scoreText;

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
       LoadScore();
        scoreText.text = "Best Score: " + MenuManager.Instance.bestPlayer + " : " + MenuManager.Instance.bestScore;
    }

    public void StartNew()
    {
        currentPlayer = inputField.text;
        currentScore = 0;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
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
}
