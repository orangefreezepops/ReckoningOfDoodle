using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController playerInstance;

    public float moveSpeed;
    private float activeMoveSpeed;//this move speed change when dash
    public float dashSpeed = 10f;
    public float dashDuration = 0.3f;
    public float dashCooldown = 1f;
    [HideInInspector]
    public float dashCounter, dashCooldownCounter;

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
        activeMoveSpeed = moveSpeed;
        dashCooldownCounter = -1;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        //control movement
        //transform.position += new Vector3(moveInput.x*Time.deltaTime*moveSpeed, moveInput.y * Time.deltaTime*moveSpeed, 0f);
        moveInput.Normalize();
        RB.velocity = moveInput * activeMoveSpeed;

        //control Aim
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);

        //if the mouse to the left of the player. set player to look left
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0, 0, angle);
        if (mousePos.x < screenPoint.x)
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

        //control dash

        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownCounter == -1)
        {
            activeMoveSpeed = dashSpeed;
            dashCounter = dashDuration;
            anim.SetTrigger("dash");
            HealthController.Instance.setDashInvinc(dashDuration);
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCooldownCounter = dashCooldown;
            }
        }

        if (dashCooldownCounter > 0)
        {
            dashCooldownCounter -= Time.deltaTime;
            if(dashCooldownCounter <= 0)
            {
                dashCooldownCounter = -1;
            }
        }





    }
}
