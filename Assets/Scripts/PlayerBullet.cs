using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 7.5f;
    public GameObject Effect;
    public int damage = 50;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(Effect, transform.position, transform.rotation);
        Destroy(gameObject);

        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(damage);
        }

        if (other.tag == "Boss")
        {
            other.GetComponent<BossController>().DamageBoss(damage);
        }
    }

    protected void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
