using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public float moveSpeed;
    public Transform target;
    public bool isfollow = false;
    private float shakeTime = 0f;
    private float shakeM = 0.1f;
    Vector3 Position;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isfollow)
        {
            if (target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
            }
            Position = transform.position;

        }
        else
        {
           
            Vector3 vec = new Vector3(PlayerController.playerInstance.transform.position.x, PlayerController.playerInstance.transform.position.y, -10);
           
        }

        if(shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            transform.position = randomVec3() * shakeM + Position ;
        }
    }
    public Vector3 randomVec3()
    {
        Vector3 vec = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        return vec;
    }
    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void Shake()
    {
        shakeTime = 0.05f;
    }
}
