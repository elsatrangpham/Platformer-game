using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        //score = 0;
        score = PlayerPrefs.GetInt("CurrentPlayerScore");
    }

    // Update is called once per frame
    void Update()
    {
        if (score < 0)
            score = 0;
        scoreText.text = "Score: " + score;
    }

    public static void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        PlayerPrefs.SetInt("CurrentPlayerScore", score);
    }

    public static void Reset()
    {
        score = 0;
        PlayerPrefs.SetInt("CurrentPlayerScore", score);
    }
}
