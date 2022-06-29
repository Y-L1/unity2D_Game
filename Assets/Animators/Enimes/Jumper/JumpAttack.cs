using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : MonoBehaviour
{
    private GameObject PlayerObj;

    private void Start()
    {
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerObj.GetComponent<PlayerHealthControll>().DeclineHeath("Jumper");
        }
    }
}
