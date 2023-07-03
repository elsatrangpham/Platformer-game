using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    float moveVelocity;
    public float jumpHeight;
    bool isGround = true;
    int isDoubleJump = 0;
    Animator anim;
    LevelManager levelManager;
    public Transform firePoint;
    public GameObject ninjaStar;
    public float shotDelay;
    float shotDelayCounter;
    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockFromRight;
    public bool onLadder;
    public float climbSpeed;
    float climbVelocity;
    float gravityStore;
    LifeManager lifeManager;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gravityStore = GetComponent<Rigidbody2D>().gravityScale;
        lifeManager = FindObjectOfType<LifeManager>();

        if (lifeManager.isSpecialHoleActive)
        {
            transform.position = new Vector2(28.5f, 1.47f);
            lifeManager.isSpecialHoleActive = false;
        }
        else
            transform.position = new Vector2(-4.42f, -0.63f); //transform.position: component chứa vị trí của game object
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump") && ((isGround == false && isDoubleJump < 2) || isGround == true))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            isGround = false;
            isDoubleJump++;
        }
        //moveVelocity = 0;
        moveVelocity = moveSpeed * Input.GetAxisRaw("Horizontal");

        if (knockbackCount < 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            if (knockFromRight)
                GetComponent<Rigidbody2D>().velocity = new Vector2(-knockback, knockback);
            if (!knockFromRight)
                GetComponent<Rigidbody2D>().velocity = new Vector2(knockback, knockback);
            knockbackCount -= Time.deltaTime;
        }

        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        anim.SetBool("Grounded", isGround);

        if (GetComponent<Rigidbody2D>().velocity.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (GetComponent<Rigidbody2D>().velocity.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
            shotDelayCounter = shotDelay;
        }

        if (Input.GetButton("Fire1"))
        {
            shotDelayCounter -= Time.deltaTime;
            
            if (shotDelayCounter <= 0)
            {
                shotDelayCounter = shotDelay;
                Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
            }
        }

        if (anim.GetBool("Sword"))
            anim.SetBool("Sword", false);
        if (Input.GetButtonDown("Fire2"))
            anim.SetBool("Sword", true);

        if (onLadder)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            climbVelocity = climbSpeed * Input.GetAxisRaw("Vertical");
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, climbVelocity);
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = gravityStore;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            isDoubleJump = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
            GetComponent<AudioSource>().Play();

        if (collision.transform.tag == "MovingPlatform")
        {
            transform.parent = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }
}
