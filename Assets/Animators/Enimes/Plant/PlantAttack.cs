using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAttack : MonoBehaviour
{
    GameObject playerObj;

    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerObj.GetComponent<PlayerHealthControll>().DeclineHeath("Plant");
        }
    }
}
