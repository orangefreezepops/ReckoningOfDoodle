using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D RB;
    public float moveSpeed;
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
    private float keepDistance = 2f;
    public float playerDistance;
    private bool faceDirection;

    public GameObject bullet;
    public Transform firePoint;
    public float fireInterval = 0.3f;
    public float rangeToFire = 4f;
    protected float fireTimer = 0;
    public AudioSource hitSFX;
    public AudioSource dieSFX;

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
            playerDistance = getPlayerDistance();
            //chase player
            if (shouldChase && playerDistance < rangeToChase && playerDistance > keepDistance)
            {
                moveDirection = PlayerController.playerInstance.transform.position - transform.position;
                if (moveDirection.x > 0) faceDirection = true;
                else faceDirection = false;
            }
            else
            {
                moveDirection = Vector3.zero;
            }

            moveDirection.Normalize();

            
            //change face direction 
            if (faceDirection)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else 
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
            fireTimer += Time.deltaTime;
            if (shouldShoot && playerDistance < rangeToFire && fireTimer >= fireInterval)
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
         bullet0=Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
         bullet0.GetComponent<EnemyBullet>().setDirection(vector);
        fireTimer = 0;

    }
    public float getPlayerDistance()
    {
        float distance = Vector3.Distance(transform.position, PlayerController.playerInstance.transform.position);
        return distance;
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
        hitSFX.Play();
        //when hit, enemy turn red
        body.color = new Color(1, 0, 0, 1);
        Instantiate(hitEffect, transform.position, transform.rotation);
        health -= points;
        if(health <= 0)
        {
            dieSFX.Play();
            Destroy(gameObject);
            Vector3 offset = new Vector3(0f, 0.5f, 0f);
            int random = Random.Range(0,2);
            int randomR = Random.Range(0, 1);
            //instanitate blood splatter
            Instantiate(deathSplatter[random], transform.position-offset, Quaternion.Euler(0f,0f,randomR*180));

        }
    }
}
