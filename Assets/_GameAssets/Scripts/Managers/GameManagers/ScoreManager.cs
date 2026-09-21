using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int Score { get => _score; private set => Score = _score; }
    private int _score;
    public void IncreaseScore(int AddScore)
    {
        _score += AddScore;
    }
    void Start()
    {
        _score = 0;
        if (instance != null)
            return;
        else
            instance = this;
    }

    
}
