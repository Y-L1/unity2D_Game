using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerControll : MonoBehaviour
{
    public Rigidbody2D RB;
    public Collider2D Coll;
    public Animator Anim;

    public Transform groundCheck;
    public LayerMask ground;//地面图层

    public float jumpForce;
    public float speed;

    public bool isGround;//是否在地面


    /**************角色功能******************/

    public bool isJump;//是否跳跃
    private bool jumpPressed;//跳跃键是否按下
    private int jumpCount;//跳跃按下的次数

    public bool isAttack;
    private bool attackPressed;

    public bool isAirAttack;
    private bool airAttackPressed;

    public bool isDash;
    private bool isDashPressed;

    public GameObject dashwind;
    public GameObject dashObj;



    public bool isCast;
    private bool isCastPressed;


    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        //InitPos();

        dashwind.SetActive(false);
        dashObj.SetActive(false);

        GameObject.Find("Player1/sword").GetComponent<BoxCollider2D>().enabled = false;

    }
    void InitPos()
    {
        transform.position = new Vector3(-1.4f, -1.42f, 0);
    }


    // Update is called once per frame
    void Update()
    {
        //jump检测
        if(Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }

        //attack检测
        if (Input.GetKeyDown(KeyCode.Mouse0 )&& Anim.GetBool("isAttackFinish"))
        {
            attackPressed = true;
            
        }
        /*
        if (Input.GetKeyDown(KeyCode.Mouse0) && Anim.GetBool("isAirAttackFinish"))
        {
            airAttackPressed = true;

        }*/

        //dash检测
        if(Input.GetKeyDown(KeyCode.S))
        {
            isDashPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            isDashPressed = false;
        }

        //cast检测
        if (Input.GetKeyDown(KeyCode.Mouse1) && transform.GetComponent<PlayerHealthControll>().playerEnergy >= 5)
        {
            isCastPressed = true;
            transform.GetComponent<PlayerHealthControll>().ReduceEnergy();
        }

    }
    private void FixedUpdate()
    {
        //地面检测
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);

        if(Cursor.visible == false)
        {
            if (Anim.GetBool("isDashing") == false)
            {
                //角色移动
                Movement();

                //角色跳跃
                Jump();
            }

            //下滑冲刺
            Dash();

            //攻击
            Attack();

            //技能释放
            Cast();

            //动画切换
            SwitchAnim();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "DeadLine")
        {
            Invoke("ReStart", 2f);
        }
    }
    void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



    void Movement()
    {
        float horizotalMove = Input.GetAxis("Horizontal");
        float faceDirection = Input.GetAxisRaw("Horizontal");
        //角色移动
        if(horizotalMove != 0)
        {
            RB.velocity = new Vector2(horizotalMove * speed * Time.deltaTime, RB.velocity.y);
        }
            
        if (faceDirection != 0)
        {
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }

        
    }

    void Jump()
    {
        if (isGround)
        {
            jumpCount = 2;
            isJump = false;
        }
        if(jumpPressed && isGround)
        {
            isJump = true;
            RB.velocity = new Vector2(RB.velocity.x, jumpForce * Time.deltaTime);
            jumpCount--;

            jumpPressed = false;

            SoundManger.instance.JumpAudio();
        }
        else if (jumpPressed && jumpCount > 0 && !isGround)
        {
            RB.velocity = new Vector2(RB.velocity.x, jumpForce * Time.deltaTime);
            jumpCount--;

            jumpPressed = false;

            SoundManger.instance.JumpAudio();
        }
    }

    void Attack()
    {
        if (isGround)
        {
            if (attackPressed)
            {
                SoundManger.instance.SwordAudio();
                //在地面点击攻击

                isAttack = true;
                attackPressed = false;
            }

        }
        else
        {
            if (attackPressed)
            {
                SoundManger.instance.SwordAudio();
                //在air点击攻击

                isAirAttack = true;
                airAttackPressed = false;
            }

        }

        //collAtk设置为false
        if(isAttack || isAirAttack)
        {
            GameObject.Find("Player1/sword").GetComponent<BoxCollider2D>().enabled = true;
        }

    }
    void Cast()
    {
        if (isCastPressed)
        {
            isCast = true;
            isCastPressed = false;
        }
        else
        {
            isCast = false;
        }
    }


    float startTime = -1f;
    float dashSpeed = 500f;
    void Dash()
    {
        if (isGround)
        {
            
            
            if (isDashPressed)
            {
                if(Mathf.Abs(RB.velocity.x) > 1)
                {
                    //dash按键按下，并且哎水平速度达到一定值
                    isDash = true;
                    isDashPressed = false;

                    //记录开始动作的时间
                    startTime = Time.time;

                    //dashwind动画开始播放
                    dashwind.SetActive(true);

                    //dash残影start
                    dashObj.SetActive(true);
                }
                

            }
            if(startTime >= 0)//dash开始时间更新，角色开始dash动作
            {
                if (Time.time - startTime < 0.5f)
                {
                    RB.velocity = new Vector2(dashSpeed * Time.deltaTime * transform.localScale.x, RB.velocity.y);
                }
                if(Time.time - startTime >= 0.5f)
                {
                    transform.GetComponent<AnimEvent>().DashFinish();
                }
            }
        }
        else
        {
            //不在地面不能dash
            isDash = false;
        }
    }

    void SwitchAnim()
    {
        //左右移动动画
        Anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        if (isGround)
        {
            Anim.SetBool("isGround", true);
        }
        else
        {
            Anim.SetBool("isGround", false);
        }

        //跳跃
        if (isGround)
        {
            Anim.SetBool("isFalling", false);
            Anim.SetBool("isJumping", false);
        }
        else if(RB.velocity.y > 0)
        {
            Anim.SetBool("isJumping", true);
        }
        else if(RB.velocity.y < 0)
        {
            Anim.SetBool("isJumping", false);
            Anim.SetBool("isFalling", true);
        }

        //地面攻击切换
        if (isAttack)
        {
            Anim.SetBool("isAttacking", true);
            
        }
        else
        {
            Anim.SetBool("isAttacking", false);
        }
        if (Anim.GetBool("isAttackFinish"))
        {
            isAttack = false;
        }

        if (isAirAttack)
        {
            Anim.SetBool("airAttack", true);

        }
        else
        {
            Anim.SetBool("airAttack", false);
        }
        if (Anim.GetBool("isAirAttackFinish"))
        {
            isAirAttack = false;
        }

        //dash
        if (isDash && Anim.GetBool("isDashing").Equals(false))
        {
            Anim.SetBool("isDashing", true);
        }
        else if (Anim.GetBool("isDashing"))
        {
            //dash后改变dash状态isdash的值

            isDash = false;
        }

        //cast
        if (isCast)
        {
            Anim.SetBool("isCasting", true);
        }
    }



}
