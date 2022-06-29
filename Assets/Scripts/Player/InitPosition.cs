using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPosition : MonoBehaviour
{
    GameObject Player;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        Player.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
