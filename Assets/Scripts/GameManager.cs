using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    static public Tilemap pucks_map { get; set; }
    static public Tilemap walls_map;

    int game_score = 0;
    Text score_text;


    // Start is called before the first frame update
    void Start()
    {
        walls_map = GameObject.FindObjectOfType<WallsTilemap>().GetComponent<Tilemap>();
        pucks_map = GameObject.FindObjectOfType<PucksTilemap>().GetComponent<Tilemap>();
        initGameScores();
        score_text = GameObject.FindObjectOfType<Canvas>().GetComponentInChildren<Text>();
    }


    // Update is called once per frame
    void Update()
    {
        initGameScores();
        Debug.Log(game_score);

    }
    private void initGameScores()
    {
        game_score = 0;
    }

    public int GetScores()
    {        
        return game_score;

    }

    public void SetScore(int value)
    {
        game_score = value;
        score_text.text = "Score: " + game_score.ToString();
    }

    public void ChangeScore(int amount)
    {
        
        SetScore(game_score + amount);
    }

    public void DeletePuckAt(Vector3Int position)
    {        
        pucks_map.SetTile(position, null);
    }
}
