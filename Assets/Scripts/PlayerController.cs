using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController playerInstance;

    public float moveSpeed;
    private float currentMoveSpeed;//this move speed change when dash

    private bool faceDirection;

    private bool canDash;
    public bool isDashing = false;
    public float dashSpeed = 10f;
    public float dashDuration = 0.3f;
    public float dashCooldown = 1f;
    public float dashTimer=0;
    public float dashCooldownTimer=1f;

    private Vector2 moveInput;
    public Rigidbody2D RB;
    public Transform gunArm;
    private Camera theCam;
    public Animator anim;
    public GameObject bulletToFire;
    public Transform firePoint;
    public Transform firePoint2;
    public SpriteRenderer bodySR;
    public bool doubleShoot = false;
    private void Awake()
    {
        playerInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
        currentMoveSpeed = moveSpeed;
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        //control movement
        moveInput.Normalize();

        float angle = getGunRotation();
        gunArm.rotation = Quaternion.Euler(0, 0, angle);
        if(faceDirection)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunArm.localScale = new Vector3(-0.6f, -0.6f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            gunArm.localScale = new Vector3(0.6f, 0.6f, 1f);
        }

        //control shoot
        if (Input.GetMouseButtonDown(0))
        {
            AudioManager.instance.PlaySFX(11);
            if (doubleShoot)
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                Instantiate(bulletToFire, firePoint2.position, firePoint2.rotation);
            }
            else
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
            }
           
        }

        if(moveInput != Vector2.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        dashCooldownTimer += Time.deltaTime;
        if (canDash)
        {
            if (Input.GetKeyDown(KeyCode.Space)&& dashTimer == 0 && dashCooldownTimer >= dashCooldown)
            {
  
                enterDash();
            }
        }

        if (isDashing)
        {
            dashTimer += Time.deltaTime;
        }
        if (dashTimer >= dashDuration) enterWalk();
        if (dashCooldownTimer >= dashCooldown) canDash = true;
        RB.velocity = moveInput * currentMoveSpeed;
    }
    void enterWalk()
    {
        currentMoveSpeed = moveSpeed;
        dashTimer = 0;
        isDashing = false;
    }
    void enterDash()
    {
        anim.SetTrigger("dash");
        currentMoveSpeed = dashSpeed;
        isDashing = true;
        canDash = false;
        dashCooldownTimer = 0;

    }
    float getGunRotation()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.localPosition);
        if (mousePosition.x < playerPosition.x) faceDirection = true;
        else faceDirection = false;
        return getAngle(mousePosition, playerPosition);
    }

    float getAngle(Vector3 v1, Vector3 v2)
    {
        return Mathf.Atan2(v1.y - v2.y, v1.x - v2.x) * Mathf.Rad2Deg;
    }


}

