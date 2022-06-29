using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveOnDia : MonoBehaviour
{
    public Rigidbody2D RB;

    private float speed = 100f;
    private bool isFloor;
    private bool isLevel;
    private bool isMovePosDir;
    // Start is called before the first frame update
    void Start()
    {
        RB = transform.GetComponent<Rigidbody2D>();
        isFloor = false;
        isMovePosDir = false;
    }

    private void FixedUpdate()
    {
        PlayerMove();

    }
    private void Update()
    {
        
        
    }
    void PlayerMove()
    {
        if (isFloor && isLevel)
        {
            if (isMovePosDir)
            {
                //平台向左移动
                RB.velocity += new Vector2(-speed * Time.deltaTime, 0);
                
            }
            else
            {
                RB.velocity += new Vector2(speed * Time.deltaTime, 0);
            }

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.transform.tag.Equals("Diamond"))
        {
            isFloor = true;
            this.isMovePosDir = collision.gameObject.GetComponent<MoveDia>().isMovePosDir;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Diamond"))
        {
            isFloor = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Diamond"))
        {
            this.isLevel = collision.gameObject.GetComponent<MoveDia>().isLevel;
            this.speed = collision.gameObject.GetComponent<MoveDia>().speed;
            //Debug.Log(speed);
        }
    }
}
