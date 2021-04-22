using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool close = false;
    public GameObject[] doors;
    public List<GameObject> enemies = new List<GameObject>();
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (!enemyExist())
            {
                openDoor();
            }
        }
    }
    private bool enemyExist()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
                return true;

        }
        return false;
    }
    private void closeDoor()
    {
      
        for(int i =0; i< doors.Length; i++)
        {
            doors[i].SetActive(true);
        }
    }
    private void openDoor()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            CameraController.instance.ChangeTarget(transform);

            if (close && enemyExist())
            {
                closeDoor();
            }
            active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            active = false;
        }
    }
}
