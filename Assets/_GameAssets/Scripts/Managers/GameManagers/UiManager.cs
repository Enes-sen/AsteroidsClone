using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    [SerializeField] private Text ScoreTxt;
    [SerializeField] private int ShipsLeft;
    [SerializeField] private Image[] Shipimages;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        ShipsLeft = Shipimages.Length;
    }
    public void UpdateScore()
    {
        ScoreTxt.text = ScoreManager.instance.Score.ToString();
        if (PlayerPrefs.GetInt("HScore") < ScoreManager.instance.Score)
        {
            PlayerPrefs.SetInt("HScore",ScoreManager.instance.Score);
        }
    }

    public int DecreaseShip()
    {
        if (ShipsLeft > 0)
        {
            ShipsLeft -= 1;
        }
        UpdateImages(ShipsLeft);
        return ShipsLeft; 
    }

    private void UpdateImages(int ships)
    {
        Shipimages[ships].gameObject.SetActive(false);
    }
}
