using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;


public class HealthManager : MonoBehaviour
{
    public int maxHealth;   
    public static int playerHealth;
    public TextMeshProUGUI healthText;
    LevelManager levelManager;
    public bool isDead;
    LifeManager lifeSystem;
    MainMenu mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        //playerHealth = maxPlayerHealth;
        playerHealth = PlayerPrefs.GetInt("PlayerCurrentHealth");
        levelManager = FindObjectOfType<LevelManager>();
        lifeSystem = FindObjectOfType<LifeManager>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0 && isDead == false)
        {
            playerHealth = 0;
            levelManager.RespawnPlayer();
            lifeSystem.TakeLife();
            PlayerPrefs.SetInt("PlayerCurrentHealth", maxHealth);
            isDead = true;
        }
        healthText.text = "Health: " + playerHealth;
    }

    public static void HurtPlayer(int damageToGive)
    {
        playerHealth -= damageToGive;
        PlayerPrefs.SetInt("PlayerCurrentHealth", playerHealth);
    }

    public void FullHealth()
    {
        playerHealth = PlayerPrefs.GetInt("PlayerCurrentHealth");
    }
}
