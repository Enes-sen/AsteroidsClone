using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUiManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Text HighScore;
    [SerializeField] private Button PlayBtn,QuitBtn;


    private void Start()
    {
        int _score = PlayerPrefs.GetInt("HScore") != 0 ? PlayerPrefs.GetInt("HScore") : 0;
        HighScore.text = "HIGH-SCORE: " + _score.ToString();
        PlayBtn.onClick.AddListener(() =>LoadGame() );
        QuitBtn.onClick.AddListener(() =>ExitGame() );

    }


    private void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
    private void ExitGame()
    {
        Application.Quit();
    }
}
