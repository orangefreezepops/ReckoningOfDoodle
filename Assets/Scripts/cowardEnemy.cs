using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cowardEnemy : EnemyController
{
    public float rangeToRun = 3f;
    public bool shouldRun = true;
    public float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        shouldShoot = true;
        shouldChase = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (body.isVisible)
        {
            playerDistance = getPlayerDistance();
            //run away
            if (shouldRun && playerDistance < rangeToRun)
            {
                moveDirection = -PlayerController.playerInstance.transform.position + transform.position;
            }
            //enter panic
            else if (playerDistance >= rangeToRun+1 && playerDistance < rangeToRun +4)
            {
                if(timer <= 0.25f)
                {
                    timer += Time.deltaTime;
                }
                if(timer > 0.25f)
                {
                    timer = 0;
                    moveDirection = randomVec3();
                }
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

            fireTimer += Time.deltaTime;
            if (shouldShoot && playerDistance < rangeToFire && fireTimer >= fireInterval)
            {
                shoot();
            }

            hitBodyEffect();
        }
    }

    public Vector3 randomVec3()
    {
        Vector3 vec = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        return vec;
    }
    protected override void shoot()
    {
        Vector2 firePointPosition = firePoint.transform.position;
        Vector2 bulletPosition = new Vector2(firePointPosition.x - 1, firePointPosition.y);
        Instantiate(bullet, bulletPosition, firePoint.transform.rotation);
        bulletPosition = new Vector2(firePointPosition.x + 1, firePointPosition.y);
        Instantiate(bullet, bulletPosition, firePoint.transform.rotation);
        bulletPosition = new Vector2(firePointPosition.x, firePointPosition.y-1);
        Instantiate(bullet, bulletPosition, firePoint.transform.rotation);
        bulletPosition = new Vector2(firePointPosition.x, firePointPosition.y + 1);
        Instantiate(bullet, bulletPosition, firePoint.transform.rotation);
        bulletPosition = new Vector2(firePointPosition.x, firePointPosition.y + 1);
        fireTimer = 0;
    }
}
