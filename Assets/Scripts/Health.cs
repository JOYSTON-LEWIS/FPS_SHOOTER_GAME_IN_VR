using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// to reference UI elements
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public float health = 100;
    public Image healthImg;
    public TextMeshProUGUI healthText;

    // we will use this damage function for bullets script

    public void Damage(int damage) 
    {
        // health reducing when damage is taken
        health -= damage;

        // fill amount is between 0 to 1
        // so define whatever health you want
        // but divide by it to get it interms of 0 to 1
        // avoiding and handling null reference
        if(healthImg)
            healthImg.fillAmount = health / 100;

        // we dont want decimals so we write f0
        // avoiding and handling null reference
        if (healthText)
            healthText.text = health.ToString("F0");

        // next to code or bullet hitting us or enemy in BulletRound.cs
    }

    public void resetHealth()
    {
        health = 100;
        if (healthImg)
            healthImg.fillAmount = 1;
        if (healthText)
            healthText.text = health.ToString("F0");
    }

    // if we want to add logic to heal the player
    // it done as follows
    /*
    public void Heal(int heal)
    {
        health += heal;

        if (healthImg)
            healthImg.fillAmount = health / 100;

        if (healthText)
            healthText.text = health.ToString("F0");

    }
    */


    // these two functions not required
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
