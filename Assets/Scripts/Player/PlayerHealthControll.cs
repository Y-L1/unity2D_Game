using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthControll : MonoBehaviour
{
    GameObject Player;
    public Image healthBar;
    public Text healthText;
    public Image energyBar;
    public Text energyText;

    public GameObject deadObj;

    public float playerHealth = 100f;
    public float playerEnergy = 100f;

    private void Start()
    {
        Player = transform.gameObject;

        deadObj.SetActive(false);
    }

    private void FixedUpdate()
    {
        ImageHealthBar();
        ImageEnergyBar();
        Dead();
    }
    void ImageHealthBar()
    {
        float nowHealth = playerHealth;
        if(playerHealth < 0)
        {
            nowHealth = 0;
        }
        healthBar.fillAmount = nowHealth / 100;
        healthText.text = nowHealth.ToString() + "/100";
    }

    void ImageEnergyBar()
    {
        float nowEnergy = playerEnergy;
        if(playerEnergy < 0)
        {
            nowEnergy = 0;
        }
        energyBar.fillAmount = nowEnergy / 100;
        energyText.text = nowEnergy.ToString() + "/100";

    }
    public void ReduceEnergy()
    {
        playerEnergy -= 5;
    }

    //不同怪物对角色造成的伤害
    public void DeclineHeath(string enemyName)
    {
        if(enemyName == "Bee")
        {
            this.playerHealth -= 10;
        }
        else if(enemyName == "Plant")
        {
            this.playerHealth -= 15;
        }
        else if(enemyName == "Jumper")
        {
            this.playerHealth -= 5;
        }
        else if(enemyName == "Crap")
        {
            this.playerHealth -= 5;
        }
    }

    public void AddHealth()
    {
        if(playerHealth < 100f)
        {
            playerHealth += 15f;
        }
        if(playerHealth > 100)
        {
            playerHealth = 100f;
        }
    }
    public void AddEnergy()
    {
        if(playerEnergy < 100f){
            playerEnergy += 15f;
        }
        if(playerEnergy > 100f)
        {
            playerEnergy = 100f;
        }
    }

    /********************Dead**************************/
    
    void Dead()
    {
        if(playerHealth <= 0)
        {
            Cursor.visible = true;
            deadObj.SetActive(true);
            Time.timeScale = 0f;
        }
    }


}
