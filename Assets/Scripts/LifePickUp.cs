using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePickUp : MonoBehaviour
{
    LifeManager lifeSystem;

    // Start is called before the first frame update
    void Start()
    {
        lifeSystem = FindObjectOfType<LifeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lifeSystem.GiveLife();
            Destroy(gameObject);
        }
    }
}
