using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject); //This is so that when the scene gets reloaded, the score resets.
        }
        else
        {
            DontDestroyOnLoad(gameObject); //This is so the score can display in the game over screen...
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue) //scoreValue is a custom parameter. For example, to add 10 points, we use AddToScore(10).
    {
        score += scoreValue;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

}
