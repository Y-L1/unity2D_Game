using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crap : MonoBehaviour
{
    GameObject playerObj;
    private Rigidbody2D crabRb;
    private Animator Anim;
    public Transform leftPoint, rightPoint;

    [SerializeField] private float speed = 100f;
    private float leftX, rightX;
    private int isWalk;
    private bool facingRight;
    private float crapHealth = 100f;

    private void Awake()
    {
        isWalk = Animator.StringToHash("isWalking");
    }

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        crabRb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();

        leftX = leftPoint.position.x;
        rightX = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);

        facingRight = false;
    }

    private void FixedUpdate()
    {
        EnemyHealthUpdate();
        DirectionController();

        if(Vector2.Distance(transform.position, playerObj.transform.position) > 5f)
        {
            ChangeState();
        }
        

        if(Anim.GetBool(isWalk) == true)
        {
            WalkState();
        }
        else
        {
            IdleState();
        }
        
    }

    void ChangeState()
    {
        int nowTime = (int)Time.time;
        switch (nowTime % 5)
        {
            case 0: Anim.SetBool(isWalk, true);break;
            case 4: Anim.SetBool(isWalk, false);break;
        }
    }

    void IdleState()
    {

    }
    void DirectionController()
    {
        float ScaleX = transform.localScale.x;
        float ScaleY = transform.localScale.y;
        float ScaleZ = transform.localScale.z;

        if (facingRight)
        {
            transform.localScale = new Vector3(-Mathf.Abs(ScaleX), ScaleY, ScaleZ);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(ScaleX), ScaleY, ScaleZ);
        }
    }
    void WalkState()
    {
        if (facingRight)
        {
            //ÏòÓÒ
            crabRb.velocity = new Vector2(speed * Time.deltaTime, crabRb.velocity.y);
            
            if(transform.position.x >= rightX)
            {
                facingRight = false;
            }
        }
        else
        {
            crabRb.velocity = new Vector2(-speed * Time.deltaTime, crabRb.velocity.y);
            if(transform.position.x <= leftX)
            {
                facingRight = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerHealthControll>().DeclineHeath("Crap");
        }
    }

    void EnemyHealthUpdate()
    {
        this.crapHealth = GetComponentInChildren<EnemyHealthController>().nowHealth;
        if (this.crapHealth <= 0)
        {
            GetComponentInChildren<EnemyHealthController>().DeathPlay();
            Destroy(transform.gameObject);
        }
    }
}
