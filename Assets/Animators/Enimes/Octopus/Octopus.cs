using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : MonoBehaviour
{
    private float octopusHealth = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        EnemyHealthUpdate();
    }


    void EnemyHealthUpdate()
    {
        this.octopusHealth = GetComponentInChildren<EnemyHealthController>().nowHealth;
        if (this.octopusHealth <= 0)
        {
            GetComponentInChildren<EnemyHealthController>().DeathPlay();
            Destroy(transform.gameObject);
        }
    }
}
