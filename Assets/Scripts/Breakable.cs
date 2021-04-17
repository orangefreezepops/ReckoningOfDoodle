using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject[] broken;
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
            if (PlayerController.playerInstance.dashCounter > 0)
            {
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
            Destroy(gameObject);
            int random2 = Random.Range(0, 5);
            for (int i = 0; i < random2; i++)
            {
                int random = Random.Range(2, broken.Length);
                Instantiate(broken[random], transform.position, transform.rotation);
            }
        }

    }
}
