using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    public static HealthController Instance;
    public int currentHealth;
    public int maxHealth;

    private bool isInvinc;
    private float invincLength= 1f;
    private float invincTimer = 1f;
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
        invincTimer += Time.deltaTime;
        flashEffect();
        if (invincTimer >= invincLength)
        outInvic();
        

    }
    public void flashEffect()
    {
        //make player flash
        if ((invincTimer < 0.8 && invincTimer > 0.6) || (invincTimer < 0.4 && invincTimer > 0.2))
        {
            PlayerController.playerInstance.bodySR.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            PlayerController.playerInstance.bodySR.color = new Color(1, 0, 0, 0.5f);
        }


    }

    public void enterInvinc()
    {
        //change player alpha value to 0.5
        PlayerController.playerInstance.bodySR.color = new Color(1, 0, 0, 0.5f);
        invincTimer = 0;
        isInvinc = true;
        hit = true;
    }
    public void outInvic()
    {
      //change player alpha value to 1
       PlayerController.playerInstance.bodySR.color = new Color(1, 1, 1, 1f);
       hit = false;
       isInvinc = false;
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
        if(!isInvinc)
        {
            hit = true;
            currentHealth--;
            AudioManager.instance.PlaySFX(10);
            enterInvinc();
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
