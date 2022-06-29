using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    private GameObject Player;
    public Animator Anim;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void AttackFinish()
    {
        Anim.SetBool("isAttackFinish", true);
        GameObject.Find("Player1/sword").GetComponent<BoxCollider2D>().enabled = false;
    }
    void AttackStart()
    {
        Anim.SetBool("isAttackFinish", false);
    }

    void AirAttackFinish()
    {
        Anim.SetBool("isAirAttackFinish", true);
        GameObject.Find("Player1/sword").GetComponent<BoxCollider2D>().enabled = false;
    }
    void AirttackStart()
    {
        Anim.SetBool("isAirAttackFinish", false);
    }


    public void DashFinish()
    {
        Anim.SetBool("isDashing", false);

        //dashwind end
        Player.GetComponent<PlayerControll>().dashwind.SetActive(false);

        //dash²ÐÓ°½áÊø
        Player.GetComponent<PlayerControll>().dashObj.SetActive(false);
    }


    
    
    void CastFinish()
    {
        Anim.SetBool("isCasting", false);
    }
}
