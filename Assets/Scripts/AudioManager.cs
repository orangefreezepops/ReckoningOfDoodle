using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource Music, loseMusic, winMusic, bossMusic;
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
        
    }
    public void PlayBoss()
    {
         Music.Stop();
         bossMusic.Play();
    }
    public void PlayLose()
    {
        Music.Stop();
        bossMusic.Stop();
        loseMusic.Play();
    }

    public void PlayWin()
    {
        Music.Stop();
        winMusic.Play();
    }

}
