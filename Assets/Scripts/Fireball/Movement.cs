using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    GameObject fireball;
    Rigidbody2D RB;
    public GameObject explosion;
    private float fireballSpeed = 800f;

    public float damage = 15f;

    void Start()
    {
        fireball = transform.gameObject;
        RB = transform.gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
        if(fireball.transform.localScale.x >0)
        {
            //fireall���ҷ���
            RB.velocity = new Vector2(fireballSpeed * Time.deltaTime, RB.velocity.y);
        }
        else
        {
            //fireall������
            RB.velocity = new Vector2(-fireballSpeed * Time.deltaTime, RB.velocity.y);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //��¼fireball��ʧ��λ��
        Vector3 posFireball = fireball.transform.position + new Vector3(0, 0.2f, 0);

        //��ײ��ǽ������
        if (collision.tag.Equals("Ground"))
        {
            Destroy(this.gameObject);

            //Play explosion
            GameObject instance = (GameObject)Instantiate(explosion, posFireball, explosion.transform.rotation);
            
        }
        else if (collision.tag.Equals("Enemy"))
        {
            //fireball�򵽵���

            Destroy(this.gameObject);

            //Play explosion
            GameObject instance = (GameObject)Instantiate(explosion, posFireball, explosion.transform.rotation);

            //����˺�TakeDamage()
            try
            {
                collision.transform.GetComponentInChildren<EnemyHealthController>().TakeDamage(this.damage);
            }
            catch(NullReferenceException e)
            {
                
            }

            


        }
    }

}
