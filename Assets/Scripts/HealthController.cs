using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    public static HealthController Instance;
    public int currentHealth;
    public int maxHealth;

    private float damageInvinc = 1f;
    private float invincCount;
    private bool hit = false;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIController.Instance.healthSlider.maxValue = maxHealth;
        UIController.Instance.healthSlider.value = currentHealth;
        UIController.Instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //if hit flag is true, player is hit and enter invinc
        if(invincCount > 0 && hit == true)
        {
            invincCount -= Time.deltaTime;

            //make player flash
            if( (invincCount<0.8 && invincCount > 0.6 )||(invincCount < 0.4 && invincCount > 0.2))
            {
                PlayerController.playerInstance.bodySR.color = new Color(1, 1, 1, 0.5f);
            }
            else
            {
                PlayerController.playerInstance.bodySR.color = new Color(1, 0, 0, 0.5f);
            }

            if(invincCount <= 0)
            {
                //change player alpha value to 1
                PlayerController.playerInstance.bodySR.color = new Color(1,1,1,1f);
                hit = false;
            }
        }
        //player dash
        else if(invincCount > 0)
        {
            invincCount -= Time.deltaTime;

            PlayerController.playerInstance.bodySR.color = new Color(1, 1, 1, 0.5f);
            if (invincCount <= 0)
            {
                //change player alpha value to 1
                PlayerController.playerInstance.bodySR.color = new Color(1, 1, 1, 1f);
            }
        }
    }
    public void setDashInvinc(float duration)
    {
        invincCount = duration;
        
    }
    public void add2MaxHealth()
    {
        maxHealth = maxHealth + 2;
        HealPlayer();
        HealPlayer();

        UIController.Instance.healthSlider.maxValue = maxHealth;
        UIController.Instance.healthSlider.value = currentHealth;
        UIController.Instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    public void DamagePlayer()
    {
        if(invincCount <= 0)
        {
            hit = true;
            currentHealth--;
            AudioManager.instance.PlaySFX(10);
            //change player alpha value to 0.5
            PlayerController.playerInstance.bodySR.color = new Color(1, 0, 0, 0.5f);
            //reset invinc time
            invincCount = damageInvinc;          
            if (currentHealth <= 0)
            {
                PlayerController.playerInstance.gameObject.SetActive(false);
                UIController.Instance.deathScreen.gameObject.SetActive(true);
                AudioManager.instance.PlaySFX(8);
                AudioManager.instance.PlayGameOver();
            }

            UIController.Instance.healthSlider.value = currentHealth;
            UIController.Instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
        }
    }

    public void HealPlayer()
    {
        if(currentHealth < maxHealth)
        {
            currentHealth++;
            UIController.Instance.healthSlider.value = currentHealth;
            UIController.Instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
        }
    }
}
