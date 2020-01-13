using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    Text scoreText;
    GameSession gameSession;

    void Start()
    {
        scoreText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
    }


    void Update()
    {
        scoreText.text = gameSession.GetScore().ToString(); //ToString converts the integer from GetScore into a string that can be stored in scoreText
    }
}
