using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyController
{
    public GameObject[] bullets;
    public int mode =0;
    public int bulletIndex;
    public bool shouldWander;
    public float modeTimer = 0;
    public float timer;
    public GameObject[] firePoints;
    public bool faceDirection;
    // Start is called before the first frame update
    void Start()
    {
        shouldShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        modeTimer += Time.deltaTime;
        if (body.isVisible)
        {
            if (mode == 0 && modeTimer <= 8.0f)
            {
                shouldChase = true;
                shouldWander = false;
                fireInterval = 0.8f;
                setMoveChase();
                if (modeTimer == 8.0f) mode = 1;
            }

            if (mode == 1 && modeTimer <= 10f)
            {
                shouldChase = false;
                shouldWander = false;
                fireInterval = 0.2f;
                moveDirection = Vector3.zero;
            }
            if (mode == 2 && modeTimer <= 18f){
                shouldChase = false;
                shouldWander = true;
                fireInterval = 0.8f;
                setMoveWander();
            }
            if(modeTimer > 18f)
            {
                mode = 0;
                modeTimer = 0;
            }

            if (faceDirection)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }

            RB.velocity = moveDirection * moveSpeed;
            fireTimer += Time.deltaTime;
            if (shouldShoot && playerDistance < rangeToFire && fireTimer >= fireInterval)
            {
                shoot();
            }

            hitBodyEffect();
        }
    }
    public void setMoveChase()
    {
        if (shouldChase && playerDistance < rangeToChase)
        {
            moveDirection = PlayerController.playerInstance.transform.position - transform.position;
            if (moveDirection.x > 0) faceDirection = true;
            else faceDirection = false;
        }
        else
        {
            moveDirection = Vector3.zero;
        }
    }

    public void setMoveWander()
    {
        if (timer <= 2f)
        {
            timer += Time.deltaTime;
        }
        if (timer > 2f)
        {
            timer = 0;
            moveDirection = randomVec3();
        }

        if (moveDirection.x > 0) faceDirection = true;
        else faceDirection = false;
    }
    public Vector3 randomVec3()
    {
        Vector3 vec = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        return vec;
    }

    protected override void shoot()
    {
       for(int i =0; i < firePoints.Length; i++)
        {

            Instantiate(bullets[mode], firePoints[i].transform.position, firePoints[i].transform.rotation);
            
        }
    }
}
