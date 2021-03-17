using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GamePreloader : MonoBehaviour
{
    AudioSource audio_source;
    AudioClip start_sound;
    AudioClip waka_sound;
    AudioClip death_sound;
    

    // Start is called before the first frame update
    void Start()
    {
        audio_source = GetComponent<AudioSource>();
        
        // Loading general sounds
        start_sound = Resources.Load<AudioClip>("Sounds/game_start") ;
        waka_sound = Resources.Load<AudioClip>("Sounds/munch_1");
        death_sound = Resources.Load<AudioClip>("Sounds/death_1");
    }


    public void StartGame()
    {
        // This is apparantly the way to let sound finish to play before doing anything else. Found it on unity forum not sure how it work
        StartCoroutine(PlayStartSound());
    }
    IEnumerator PlayStartSound()
    {
        audio_source.PlayOneShot(start_sound);        
        yield return new WaitForSeconds(start_sound.length);

        // Sound finished load the maze
        SceneManager.LoadScene("SceneMainLevel");
    }
    public void PlayMunch()
    {
        if(!audio_source.isPlaying)
        {
            audio_source.PlayOneShot(waka_sound);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
