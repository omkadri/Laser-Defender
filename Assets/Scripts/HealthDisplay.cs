using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    Text healthText;
    Player player;

    void Start()
    {
        healthText = GetComponent<Text>();
        player = FindObjectOfType<Player>();
    }


    void Update()
    {
        healthText.text = player.GetHealth().ToString(); //ToString converts the integer from GetScore into a string that can be stored in scoreText
    }
}
