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
    [SerializeField] private AudioClip _btnClip;
    [SerializeField] private AudioSource _audioSrc;


    private void Start()
    {
        int _score = PlayerPrefs.GetInt("HScore") != 0 ? PlayerPrefs.GetInt("HScore") : 0;
        HighScore.text = "HIGH-SCORE: " + _score.ToString();
        PlayBtn.onClick.AddListener(() =>LoadGame() );
        QuitBtn.onClick.AddListener(() =>ExitGame() );

    }


    private void LoadGame()
    {
        StartCoroutine(LoadGame(0.5f));
    }
    private void ExitGame()
    {
        StartCoroutine(CloseGame(0.5f));
    }
    private IEnumerator LoadGame(float time)
    {
        _audioSrc?.PlayOneShot(_btnClip);
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Game");
    }
    private IEnumerator CloseGame(float time)
    {
        _audioSrc?.PlayOneShot(_btnClip);
        yield return new WaitForSeconds(time);
        Application.Quit();
    }
}
