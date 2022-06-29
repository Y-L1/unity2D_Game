using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Slug : MonoBehaviour
{
    public Transform leftPoint, rightPoint;
    private Rigidbody2D plugRb;
    [SerializeField] private float speed = 50f;

    private float leftX, rightX;
    private bool facingRight;

    public float slugHealth = 100f;

    void Start()
    {
        Init();
        plugRb = transform.GetComponent<Rigidbody2D>();
        facingRight = false;
    }

    void Init()
    {
        leftX = leftPoint.position.x;
        rightX = rightPoint.position.x;

        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }


    private void FixedUpdate()
    {
        EnemyHealthUpdate();


        if (facingRight)
        {
            //向右移动
            plugRb.velocity = new Vector2(speed * Time.deltaTime, plugRb.velocity.y);

            if(transform.position.x >= rightX)
            {
                facingRight = false;
                transform.localScale = new Vector3(1, 1, 1);
                
            }
            
        }
        else
        {
            //向左移动
            plugRb.velocity = new Vector2(-speed * Time.deltaTime, plugRb.velocity.y);
            if(transform.position.x <= leftX)
            {
                facingRight = true;
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnemyHealthUpdate()
    {
        this.slugHealth = GetComponentInChildren<EnemyHealthController>().nowHealth;
        if (this.slugHealth <= 0)
        {
            GetComponentInChildren<EnemyHealthController>().DeathPlay();
            Destroy(transform.gameObject);
        }
    }

}
