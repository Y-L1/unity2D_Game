using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float swordDamage = 10f;

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            
            collision.transform.GetComponentInChildren<EnemyHealthController>().TakeDamage(swordDamage);
        }
    }


}
