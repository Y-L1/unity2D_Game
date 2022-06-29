using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public Transform limitPoint;
    public PolygonCollider2D attackColl;
    public Rigidbody2D jumperRb;
    private Animator Anim;
    private GameObject PlayerObj;
    private float Radius;
    private float jumpForce = 700f;

    private float jumperHealth = 100f;

    private int isJump = Animator.StringToHash("isJumping");
    void Start()
    {
        Radius = Mathf.Abs(limitPoint.position.x - transform.position.x);
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
        Anim = GetComponent<Animator>();

        
    }
    private void FixedUpdate()
    {
        EnemyHealthUpdate();
        Attack();

    }
    

    void Attack()
    {
        Vector2 dir = (PlayerObj.transform.position - transform.position).normalized;

        if(Mathf.Abs(PlayerObj.transform.position.x - transform.position.x) <= Radius)
        {
            //ÔÚ·¶Î§ÄÚ¹¥»÷
            if (!Anim.GetBool(isJump))
            {
                jumperRb.velocity = new Vector2(jumperRb.velocity.x, jumpForce * Time.deltaTime);
                Anim.SetBool(isJump, true);
            }
            
            if (jumperRb.velocity.y < 0)
            {
                jumperRb.velocity = dir * jumpForce * Time.deltaTime;
            }
        }

        if(Vector2.Distance(PlayerObj.transform.position,transform.position) <= 0.3f)
        {
            Anim.SetBool(isJump, false);
            jumperRb.velocity = Vector2.zero;
            jumperRb.velocity = -dir * jumpForce * Time.deltaTime;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            Anim.SetBool(isJump, false);
        }
    }

    void EnemyHealthUpdate()
    {
        this.jumperHealth = GetComponentInChildren<EnemyHealthController>().nowHealth;
        if (this.jumperHealth <= 0)
        {
            GetComponentInChildren<EnemyHealthController>().DeathPlay();
            Destroy(transform.gameObject);
        }
    }
}
