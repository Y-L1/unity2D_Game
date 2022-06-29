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
    public LayerMask ground;//����ͼ��

    public float jumpForce;
    public float speed;

    public bool isGround;//�Ƿ��ڵ���


    /**************��ɫ����******************/

    public bool isJump;//�Ƿ���Ծ
    private bool jumpPressed;//��Ծ���Ƿ���
    private int jumpCount;//��Ծ���µĴ���

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
        //jump���
        if(Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }

        //attack���
        if (Input.GetKeyDown(KeyCode.Mouse0 )&& Anim.GetBool("isAttackFinish"))
        {
            attackPressed = true;
            
        }
        /*
        if (Input.GetKeyDown(KeyCode.Mouse0) && Anim.GetBool("isAirAttackFinish"))
        {
            airAttackPressed = true;

        }*/

        //dash���
        if(Input.GetKeyDown(KeyCode.S))
        {
            isDashPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            isDashPressed = false;
        }

        //cast���
        if (Input.GetKeyDown(KeyCode.Mouse1) && transform.GetComponent<PlayerHealthControll>().playerEnergy >= 5)
        {
            isCastPressed = true;
            transform.GetComponent<PlayerHealthControll>().ReduceEnergy();
        }

    }
    private void FixedUpdate()
    {
        //������
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);

        if(Cursor.visible == false)
        {
            if (Anim.GetBool("isDashing") == false)
            {
                //��ɫ�ƶ�
                Movement();

                //��ɫ��Ծ
                Jump();
            }

            //�»����
            Dash();

            //����
            Attack();

            //�����ͷ�
            Cast();

            //�����л�
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
        //��ɫ�ƶ�
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
                //�ڵ���������

                isAttack = true;
                attackPressed = false;
            }

        }
        else
        {
            if (attackPressed)
            {
                SoundManger.instance.SwordAudio();
                //��air�������

                isAirAttack = true;
                airAttackPressed = false;
            }

        }

        //collAtk����Ϊfalse
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
                    //dash�������£����Ұ�ˮƽ�ٶȴﵽһ��ֵ
                    isDash = true;
                    isDashPressed = false;

                    //��¼��ʼ������ʱ��
                    startTime = Time.time;

                    //dashwind������ʼ����
                    dashwind.SetActive(true);

                    //dash��Ӱstart
                    dashObj.SetActive(true);
                }
                

            }
            if(startTime >= 0)//dash��ʼʱ����£���ɫ��ʼdash����
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
            //���ڵ��治��dash
            isDash = false;
        }
    }

    void SwitchAnim()
    {
        //�����ƶ�����
        Anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        if (isGround)
        {
            Anim.SetBool("isGround", true);
        }
        else
        {
            Anim.SetBool("isGround", false);
        }

        //��Ծ
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

        //���湥���л�
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
            //dash��ı�dash״̬isdash��ֵ

            isDash = false;
        }

        //cast
        if (isCast)
        {
            Anim.SetBool("isCasting", true);
        }
    }



}
