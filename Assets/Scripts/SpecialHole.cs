using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialHole : MonoBehaviour
{
    LifeManager lifeManager;
    // Start is called before the first frame update
    void Start()
    {
        lifeManager = FindObjectOfType<LifeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Special Hole");
            lifeManager.isSpecialHoleActive = true;
        }
    }
}
