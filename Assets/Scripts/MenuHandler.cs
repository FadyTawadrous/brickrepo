using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public TextMeshProUGUI inputField;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        MenuManager.Instance.LoadScore();
        scoreText.text = "Best Score: " + MenuManager.Instance.bestPlayer + " : " + MenuManager.Instance.bestScore;
    }

    public void StartNew()
    {
        MenuManager.Instance.currentPlayer = inputField.text;
        MenuManager.Instance.currentScore = 0;
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
}
