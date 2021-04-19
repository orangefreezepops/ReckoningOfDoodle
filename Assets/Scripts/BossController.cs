using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    private int random = 0;
    public static BossController instance;
    public BossAction[] actions;
    private int currentAction;
    private float actionCounter;
    private float shotCounter;
    private float movingDirectionCounter;
    private float movingDirectionInterval = 1.5f;
    private Vector2 faceDirection;
    public Rigidbody2D theRB;
    public Vector2 moveDirection;
    public Animator anim;
    public int currentHealth;
    public GameObject deathEffect;
    

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        actionCounter = actions[currentAction].actionLength;
        movingDirectionCounter = movingDirectionInterval;
        UIController.Instance.bossHealthBar.gameObject.SetActive(true);
        UIController.Instance.bossHealthBar.maxValue = currentHealth;
        UIController.Instance.bossHealthBar.value = currentHealth;
    }

 
    // Update is called once per frame
    void Update()
    {
        
        if (actionCounter > 0)
        {
            actionCounter -= Time.deltaTime;
            moveDirection = Vector2.zero;
            if (actions[currentAction].shouldMove)
            {
                if (actions[currentAction].shouldChase)
                {
                    moveDirection = PlayerController.playerInstance.transform.position - transform.position;
                    moveDirection.Normalize();
                    if (moveDirection.x > 0)
                    {
                        transform.localScale = new Vector3(-1f, 1f, 1f);
                    }
                    else if (moveDirection.x < 0)
                    {
                        transform.localScale = new Vector3(1f, 1f, 1f);
                    }

                }

                if (actions[currentAction].moveToPoints)
                {
                    
                    movingDirectionCounter -= Time.deltaTime;
                    if(movingDirectionCounter <=0)
                    {
                       random = Random.Range(0, 4);
                       movingDirectionCounter = movingDirectionInterval;
                    }
                    moveDirection = actions[currentAction].pointToMoveTo[random].position - transform.position;
                    if (moveDirection.magnitude < 0.3)
                        moveDirection = Vector2.zero;
                    moveDirection.Normalize();
                    faceDirection = PlayerController.playerInstance.transform.position - transform.position;
                    if (faceDirection.x > 0)
                    {
                        transform.localScale = new Vector3(-1f, 1f, 1f);
                    }
                    else if (faceDirection.x < 0)
                    {
                        transform.localScale = new Vector3(1f, 1f, 1f);
                    }

                }
            }
            theRB.velocity = moveDirection * actions[currentAction].moveSpeed;



            if (actions[currentAction].shouldShoot)
            {
                shotCounter -= Time.deltaTime;
                if(shotCounter <= 0)
                {
                    shotCounter = actions[currentAction].timeBetweenShoots;
                    foreach(Transform t in actions[currentAction].shootPoints)
                    {
                        Instantiate(actions[currentAction].itemToShoot, t.position, t.rotation);
                    }
                }
            }
        }
        else
        {
            currentAction++;
            if(currentAction >= actions.Length)
            {
                currentAction = 0;
            }

            actionCounter = actions[currentAction].actionLength;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        AudioManager.instance.PlaySFX(2);
        currentHealth -= damageAmount;
        UIController.Instance.bossHealthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            AudioManager.instance.PlaySFX(1);
            Instantiate(deathEffect, transform.position, transform.rotation);
            gameObject.SetActive(false);
            UIController.Instance.bossHealthBar.gameObject.SetActive(false);
            UIController.Instance.winScreen.gameObject.SetActive(true);
            AudioManager.instance.PlayWin();
        }
        
    }
}

[System.Serializable]
public class BossAction
{
    [Header("Action")]
    public float actionLength;
    public bool shouldMove;
    public bool shouldChase;
    public float moveSpeed;
    public bool moveToPoints;
    public Transform[] pointToMoveTo;

    public bool shouldShoot;
    public GameObject itemToShoot;
    public float timeBetweenShoots;
    public Transform[] shootPoints;
}
