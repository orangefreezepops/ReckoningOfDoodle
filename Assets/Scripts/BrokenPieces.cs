using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPieces : MonoBehaviour
{
    private Vector3 moveDirection;

    public float deceleration = 5f;
    private float time = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        moveDirection.x = Random.Range(-4, 4);
        moveDirection.y = Random.Range(-4, 4);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * Time.deltaTime;

        moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.deltaTime);

        time -= Time.deltaTime;
        if(time <= 0)
        {
            Destroy(gameObject);
        }


    }
}