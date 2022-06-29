using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Box : MonoBehaviour
{
    [SerializeField] private GameObject BoxUI;
    [SerializeField] private GameObject item;

    private GameObject Player;
    private Animator Anim;
    private int isOpen = Animator.StringToHash("open");

    private bool canOpen = false;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Anim = GetComponent<Animator>();
        Anim.SetBool(isOpen, false);

    }

    private void Update()
    {
        //箱子开关控制
        CheckOpen();

        //箱子UI
        BoxUIController();


        
    }




    private void BoxUIController()
    {
        if (Anim.GetBool(isOpen))
        {
            BoxUI.SetActive(true);
        }
        else
        {
            BoxUI.SetActive(false);
        }
    }

    void CheckOpen()
    {
        if (canOpen)
        {
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                SoundManger.instance.OpenBoxAudio();
                Anim.SetBool(isOpen, true);
            }
        }
    }

  


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canOpen = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (Anim.GetBool(isOpen))
            {
                SoundManger.instance.CloseBoxAudio();
            }
            canOpen = false;
            Anim.SetBool(isOpen, false);
        }
    }
}
