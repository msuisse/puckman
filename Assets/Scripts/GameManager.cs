using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{    
    static public Tilemap walls_map;
    static public Tilemap pucks_map { get; set; }
    int game_score;


    // Start is called before the first frame update
    void Start()
    {
        walls_map = GameObject.FindObjectOfType<WallsTilemap>().GetComponent<Tilemap>();
        pucks_map = GameObject.FindObjectOfType<PucksTilemap>().GetComponent<Tilemap>();
        initGameScores();
    }


    // Update is called once per frame
    void Update()
    {
        initGameScores();

    }
    private void initGameScores()
    {
        if (game_score == null)
        {
            game_score = 0;
        }
    }

    public int GetScores()
    {        
        return game_score;

    }

    public void SetScore(int value)
    {
        game_score = value;
    }

    public void ChangeScore(int amount)
    {
        SetScore(game_score + amount);
    }

    static public void DeletePuckAt(Vector3Int position)
    {        
        pucks_map.SetTile(position, null);
    }
}
