using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float moveSpeed;
    public bool moveRight;
    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    bool hittingWall;
    bool notAtEdge;
    public Transform edgeCheck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
        notAtEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);

        if (hittingWall || !notAtEdge)
            moveRight = !moveRight;

        if (moveRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
