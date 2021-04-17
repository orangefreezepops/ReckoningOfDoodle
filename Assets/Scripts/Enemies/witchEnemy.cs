using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class witchEnemy : EnemyController
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (body.isVisible)
        {
            if (shouldChase && Vector3.Distance(transform.position, PlayerController.playerInstance.transform.position) < rangeToChase)
            {
                moveDirection = PlayerController.playerInstance.transform.position - transform.position;
            }
            else if (Vector3.Distance(transform.position, PlayerController.playerInstance.transform.position) >= rangeToChase)
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


    protected override void shoot()
    {
        fireCounter -= Time.deltaTime;
        if (fireCounter <= 0)
        {
            fireCounter = fireRate;
            GameObject bullet0;
            Vector3 vector = new Vector3(1, 1, 1);
            Vector2 firePointPosition = firePoint.transform.position;
            Vector2 bulletPosition = new Vector2(firePointPosition.x , firePointPosition.y);
            bullet0 = Instantiate(bullet, bulletPosition, firePoint.transform.rotation);
            bullet0.GetComponent<EnemyBullet>().setDirection(vector);
        }
    }
}
