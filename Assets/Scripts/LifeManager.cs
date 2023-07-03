using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    //public int startingLives;
    int lifeCounter;
    TextMeshProUGUI theText;
    public GameObject gameOverScreen;
    public PlayerController player;
    public string mainMenu;
    public float waitAfterGameOver;
    public bool isSpecialHoleActive;

    // Start is called before the first frame update
    void Start()
    {
        theText = GetComponent<TextMeshProUGUI>();
        lifeCounter = PlayerPrefs.GetInt("PlayerCurrentLives");
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeCounter == 0 && isSpecialHoleActive == true)
        {
            SceneManager.LoadScene("Level 3");
        }
        else if (lifeCounter == 0)
        {
            gameOverScreen.SetActive(true);
            player.gameObject.SetActive(false);
        }
        theText.text = "x " + lifeCounter;

        if (gameOverScreen.activeSelf)
            waitAfterGameOver -= Time.deltaTime;
        if (waitAfterGameOver < 0)
            SceneManager.LoadScene(mainMenu);
    }

    public void GiveLife()
    {
        lifeCounter++;
        PlayerPrefs.SetInt("PlayerCurrentLives", lifeCounter);
    }

    public void TakeLife()
    {
        lifeCounter--;
        PlayerPrefs.SetInt("PlayerCurrentLives", lifeCounter);
    }
}
