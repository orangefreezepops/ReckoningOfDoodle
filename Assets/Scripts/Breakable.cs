using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject[] broken;
    public float dropPotion = 0.1f;
    public float dropCoin = 0.5f;
    public GameObject healPotion;
    public GameObject Coin;
    public AudioSource breakSFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (PlayerController.playerInstance.isDashing)
            {
              
                float r = Random.Range(0,1f);
                if (r < dropPotion)
                {
                    Instantiate(healPotion, transform.position, transform.rotation);
                }
                else if (r < dropCoin)
                {
                    Instantiate(Coin, transform.position, transform.rotation);

                }
                breakSFX.Play();
                Destroy(gameObject);
              
                int random2 = Random.Range(0, 5);
                for (int i = 0; i < random2; i++) 
                {
                    int random = Random.Range(0, broken.Length);
                    Instantiate(broken[random], transform.position, transform.rotation);
                }
            }
        }
        if(other.tag == "PlayerBullet")
        {
            breakSFX.Play();
            Destroy(gameObject);
            float r = Random.Range(0, 1f);
            if (r < dropPotion)
            {
                Instantiate(healPotion, transform.position, transform.rotation);
            }
            else if (r < dropCoin)
            {
                Instantiate(Coin, transform.position, transform.rotation);

            }
            int random2 = Random.Range(0, 5);
            for (int i = 0; i < random2; i++)
            {
                int random = Random.Range(2, broken.Length);
                Instantiate(broken[random], transform.position, transform.rotation);
            }
        }

    }
}
