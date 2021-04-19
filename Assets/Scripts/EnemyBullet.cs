using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed=4f;
    public bool isRotate;
    public bool isChase;
    public bool bigDamage;
    public Vector3 direction;
    public float rotationSpeed = 60f;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerController.playerInstance.transform.poisiton will return the position of the player
        direction = PlayerController.playerInstance.transform.position - transform.position;
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isRotate)
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        if (isChase)
        {
            direction += (PlayerController.playerInstance.transform.position - transform.position) / 500;
            direction.Normalize();
        }
        transform.position += direction * speed * Time.deltaTime;

    }

    public void setDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            HealthController.Instance.DamagePlayer();
            if (bigDamage){
                HealthController.Instance.DamagePlayer();
            }
        }

        Destroy(gameObject);

    }
    protected void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
