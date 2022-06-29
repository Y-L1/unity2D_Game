using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plant : MonoBehaviour
{
    public GameObject  attackColl;

    private GameObject playerObj;
    private Animator Anim;
    private float limitDis = 2f;

    public float plantHealth = 100f;


    void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        FacingDirection();
        SetAttckColl();
        PlantAttackBehavior();

        //敌人血量更新
        EnemyHealthUpdate();
    }

    void FacingDirection()
    {
        float ScaleX = transform.localScale.x;
        float ScaleY = transform.localScale.y;
        float ScaleZ = transform.localScale.z;

        if(playerObj.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(ScaleX), ScaleY, ScaleZ);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(ScaleX), ScaleY, ScaleZ);
        }
    }


    void PlantAttackBehavior()
    {
        float limAttack = Vector2.Distance(playerObj.transform.position, transform.position);
        if (limAttack < limitDis)
        {
            Anim.SetBool("isAttack", true);
        }
        else
        {
            Anim.SetBool("isAttack", false);
        }
    }

    void SetAttckColl()
    {
        bool isAttack = Anim.GetBool("isAttack");
        if (isAttack)
        {
            attackColl.SetActive(true);
        }
        else
        {
            attackColl.SetActive(false);
        }
    }

    void EnemyHealthUpdate()
    {
        this.plantHealth = GetComponentInChildren<EnemyHealthController>().nowHealth;
        if(this.plantHealth <= 0)
        {
            GetComponentInChildren<EnemyHealthController>().DeathPlay();
            Destroy(transform.gameObject);
        }
    }
}
