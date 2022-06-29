using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDia : MonoBehaviour
{

    public Transform leftPoint;
    public Transform rightPoint;
    public Transform topPoint;
    public Transform bottomPoint;
    private Rigidbody2D RB;
    public bool isLevel = true;
    public float speed = 100f;

    private float leftX, rightX;
    private float topY, bottomY;
    public bool isMovePosDir { get; set; }




    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        InitPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Movement();
    }

    void InitPos()
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


    void Movement()
    {
        if (isLevel)
        {//水平移动
            
            RB.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            if (isMovePosDir)
            {
                
                RB.velocity = new Vector2(-speed * Time.deltaTime, RB.velocity.y);
                if(transform.position.x < leftX)
                {
                    isMovePosDir = false;
                }
            }
            else
            {
                RB.velocity = new Vector2(speed * Time.deltaTime, RB.velocity.y);
                if(transform.position.x > rightX)
                {
                    isMovePosDir = true;
                }
            }
            
        }
        else
        {
            //上下移动
            RB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            if (isMovePosDir)
            {
                //水平移动

                RB.velocity = new Vector2(RB.velocity.x, speed * Time.deltaTime);
                if (transform.position.y > topY)
                {
                    isMovePosDir = false;
                }
            }
            else
            {
                RB.velocity = new Vector2(RB.velocity.x, -speed * Time.deltaTime);
                if (transform.position.y < bottomY)
                {
                    isMovePosDir = true;
                }
            }
        }                                                                   
    }






}
