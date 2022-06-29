using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    private float maxHealth = 100f;
    public float nowHealth = 100f;

    public GameObject HealthBG;
    public GameObject Health;
    private GameObject playerObj;
    public GameObject deathEffectObj;
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        HealthBarUpdate();
        //BloodHidden();

    }

    void HealthBarUpdate()
    {
        Health.transform.localScale = new Vector2(nowHealth / maxHealth, Health.transform.localScale.y);
        Health.transform.position = new Vector2(HealthBG.transform.position.x - (1 - nowHealth / maxHealth) / 2,Health.transform.position.y);
    }

    public void TakeDamage(float damage)
    {
        SoundManger.instance.EnemyHurtPlay();
        nowHealth -= damage;

        if(nowHealth < 0)
        {
            nowHealth = 0f;
        }
        transform.parent.GetComponentInChildren<BooldEffect>().EffectPlay();
    }
    public void DeathPlay()
    {
        GameObject instance = Instantiate<GameObject>(deathEffectObj, transform.parent.parent);

        instance.transform.position = transform.position + new Vector3(0,-1,0);
        Destroy(instance, 1f);
    }

    /*
    void BloodHidden()
    {
        SpriteRenderer[] sprites = transform.GetComponentsInChildren<SpriteRenderer>();
        float dis = Vector2.Distance(playerObj.transform.position, transform.position);
        Debug.Log(dis);
        if(dis >= 5f)
        {
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.sortingLayerName = "Default";
            }
        }
        else
        {
            foreach (SpriteRenderer sprite in sprites)
            {
                sprite.sortingLayerName = "Effect";
            }
        }

        
    }*/
}
