using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D RB;
    public float moveSpeed = 3f;
    public float rangeToChase = 5f;
    protected Vector3 moveDirection;

    //death&hit effects
    public GameObject[] deathSplatter;
    public GameObject hitEffect;
    public Animator anim;
    protected float hitEffectduration = 0.1f;
    
    //default health
    public int health = 150;

    public bool shouldChase;
    public bool shouldShoot; 

    public GameObject bullet;
    public Transform firePoint;
    public float fireRate = 0.3f;
    public float rangeToFire = 4f;
    protected float fireCounter;

    //the image of the enemy
    public SpriteRenderer body;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        //check if the enemy is being rendered
        if(body.isVisible)
        {
            //chase player
            if (shouldChase && Vector3.Distance(transform.position, PlayerController.playerInstance.transform.position) < rangeToChase)
            {
                moveDirection = PlayerController.playerInstance.transform.position - transform.position;
            }
            else
            {
                moveDirection = Vector3.zero;
            }

            moveDirection.Normalize();


            //change face direction 
            if (moveDirection.x > 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (moveDirection.x < 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }

            RB.velocity = moveDirection * moveSpeed;

            //change animation
            if (RB.velocity != Vector2.zero)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }

            //shoot

            if (shouldShoot && Vector3.Distance(transform.position, PlayerController.playerInstance.transform.position) < rangeToFire)
            {
                shoot();
            }

            hitBodyEffect();
        }
    }
    protected virtual void shoot()
    {
        GameObject bullet0;
        Vector3 vector = new Vector3(1, 1, 1);
        fireCounter -= Time.deltaTime;
        if (fireCounter <= 0)
        {
            fireCounter = fireRate;
            bullet0=Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
           bullet0.GetComponent<EnemyBullet>().setDirection(vector);
           
        }
    }
    public void hitBodyEffect()
    {
        //hit effect
        if (body.color.g == 0)
        {
            hitEffectduration -= Time.deltaTime;
            if (hitEffectduration <= 0)
            {
                //color become normal
                body.color = new Color(1, 1, 1, 1);
                hitEffectduration = 0.1f;
            }
        }
    }

    public void DamageEnemy(int points)
    {
        //when hit, enemy turn red
        body.color = new Color(1, 0, 0, 1);
        Instantiate(hitEffect, transform.position, transform.rotation);
        health -= points;
        if(health <= 0)
        {
            Destroy(gameObject);
            Vector3 offset = new Vector3(0f, 0.5f, 0f);
            int random = Random.Range(0,2);
            int randomR = Random.Range(0, 1);
            //instanitate blood splatter
            Instantiate(deathSplatter[random], transform.position-offset, Quaternion.Euler(0f,0f,randomR*180));

        }
    }
}
