﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D RB;
    public float moveSpeed;
    public float rangeToChase;
    private Vector3 moveDirection;

    //death&hit effects
    public GameObject[] deathSplatter;
    public GameObject hitEffect;
    public Animator anim;
    private float hitEffectduration = 0.1f;
    
    //default health
    public int health = 150;

    public bool shouldShoot; // this determine whether a type of enemy will shoot player
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    public float rangeToFire;
    private float fireCounter;

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
            if (Vector3.Distance(transform.position, PlayerController.playerInstance.transform.position) < rangeToChase)
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
                fireCounter -= Time.deltaTime;
                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
                }
            }

            //hit effect
            if(body.color.g == 0)
            {
                hitEffectduration -= Time.deltaTime;
                if(hitEffectduration <= 0)
                {
                    //color become normal
                    body.color = new Color(1, 1, 1, 1);
                    hitEffectduration = 0.1f;
                }
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
            Instantiate(deathSplatter[random], transform.position-offset, Quaternion.Euler(0f,0f,randomR*180));
        }
    }
}
