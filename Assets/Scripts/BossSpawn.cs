using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public GameObject boss;
    public Transform bossLocation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            AudioManager.instance.PlayBossMusic();
            Instantiate(boss, bossLocation.position, bossLocation.rotation);
            Destroy(gameObject);
        }
        
    }
}
