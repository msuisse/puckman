using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GamePreloader : MonoBehaviour
{
    AudioSource StartSound;
    // Start is called before the first frame update
    void Start()
    {
        StartSound = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        // This is apparantly the way to let sound finish to play before doing anything else. Found it on unity forum not sure how it work
        StartCoroutine(PlayStartSound());
    }
    IEnumerator PlayStartSound()
    {
        StartSound.Play();        
        yield return new WaitForSeconds(StartSound.clip.length);

        // Sound finished load the maze
        SceneManager.LoadScene("SceneMainLevel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
