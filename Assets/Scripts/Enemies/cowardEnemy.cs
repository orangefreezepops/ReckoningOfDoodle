using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cowardEnemy : EnemyController
{
    public float rangeToRun = 4f;
    public bool shouldRun = true;
    // Start is called before the first frame update
    void Start()
    {
        shouldShoot = false;
        shouldChase = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (body.isVisible)
        {
            if (shouldRun && Vector3.Distance(transform.position, PlayerController.playerInstance.transform.position) < rangeToRun)
            {
                moveDirection = -PlayerController.playerInstance.transform.position + transform.position;
            }
            else if(Vector3.Distance(transform.position, PlayerController.playerInstance.transform.position) >= rangeToRun+1)
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

            hitBodyEffect();
        }
    }
}
