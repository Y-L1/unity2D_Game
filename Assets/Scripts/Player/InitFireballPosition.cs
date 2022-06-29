using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitFireballPosition : MonoBehaviour
{

    public GameObject fireball;

    private Vector3 fireballPos;
    private Transform Player;

    Vector3 distance = new Vector3(0.639f, -0.742f, 0);


    void Start()
    {
        Player = transform;
    }
    private void FixedUpdate()
    {
        
    }

    //更新炮弹位置,及反转炮弹
    public void InitTransFireball()
    {
        
        if (Player.localScale.x > 0)
        {
            fireballPos =Player.position +  distance;
            GameObject instance = (GameObject)Instantiate(fireball, fireballPos, fireball.transform.rotation);
            
        }
        else
        {
            fireballPos = Player.position + new Vector3(-distance.x, distance.y, distance.z);
            GameObject instance = (GameObject)Instantiate(fireball, fireballPos, fireball.transform.rotation);
            instance.transform.localScale *= -1;
        }
        SoundManger.instance.FireAudio();
    }

}
