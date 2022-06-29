using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    GameObject playerObj;

    public float speed = 200f;
    public Transform leftPoint, rightPoint,topPoint,bottomPoint;
    

    private Transform startBee;
    private Rigidbody2D beeRb;
    private float leftX,rightX,topY,bottomY;
    public float limFollow =4f;

    bool isFaceRight;
    bool isFaceUp;

    public float beeHealth = 100f;
    


    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");

        startBee = transform;
        beeRb = transform.GetComponent<Rigidbody2D>();
        isFaceRight = false;
        isFaceUp = false;

        InitLimit();
    }

    

    private void FixedUpdate()
    {

        //血量更新
        EnemyHealthUpdate();


        float dis = Vector2.Distance(startBee.position, transform.position);
        if(dis > limFollow)
        {
            
        }
        else
        {
            FollowUp();
        }


        FreeFly();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerObj.GetComponent<PlayerHealthControll>().DeclineHeath("Bee");
        }
    }


    void FreeFly()
    {
        if (isFaceRight)
        {
            //向右飞行
            beeRb.velocity = new Vector2(speed * Time.deltaTime, beeRb.velocity.y);
            if (transform.position.x >= rightX)
            {
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
                isFaceRight = false;
            }
        }

        else
        {
            //向左飞行
            beeRb.velocity = new Vector2(-speed * Time.deltaTime, beeRb.velocity.y);
            if (transform.position.x <= leftX)
            {
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
                isFaceRight = true;
            }
        }

        if (isFaceUp)
        {
            //向上飞行
            beeRb.velocity = new Vector2(beeRb.velocity.x, speed * Time.deltaTime);
            if (transform.position.y >= topY)
            {
                isFaceUp = false;
            }
        }
        else
        {
            beeRb.velocity = new Vector2(beeRb.velocity.x, -speed * Time.deltaTime);
            if (transform.position.y <= bottomY)
            {
                isFaceUp = true;
            }
        }
    }

    //追击角色
    void FollowUp()
    {

    }




    void InitLimit()
    {
        leftX = leftPoint.position.x;
        rightX = rightPoint.position.x;
        topY = topPoint.position.y;
        bottomY = bottomPoint.position.y;

        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
        Destroy(topPoint.gameObject);
        Destroy(bottomPoint.gameObject);
    }

    void EnemyHealthUpdate()
    {
        this.beeHealth = GetComponentInChildren<EnemyHealthController>().nowHealth;
        if (this.beeHealth <= 0)
        {
            GetComponentInChildren<EnemyHealthController>().DeathPlay();
            Destroy(transform.gameObject);
        }
    }
}
