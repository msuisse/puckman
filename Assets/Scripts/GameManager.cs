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

    static private float default_velocity = 3f;
    int game_score = 0;
    Text score_text;
    private int pucks_count;


    // Start is called before the first frame update
    void Start()
    {
        walls_map = GameObject.FindObjectOfType<WallsTilemap>().GetComponent<Tilemap>();
        pucks_map = GameObject.FindObjectOfType<PucksTilemap>().GetComponent<Tilemap>();
        initGameScores();
        score_text = GameObject.FindObjectOfType<Canvas>().GetComponentInChildren<Text>();
        pucks_count = InitPucksCounter();


    }

    // Update is called once per frame
    void Update()
    {
        if(pucks_count == 0)
        {
            Debug.Log("WIN");
        }

    }

    private int InitPucksCounter()
    {
        int pucks = 0;
        BoundsInt bounds = pucks_map.cellBounds;
        TileBase[] allTiles = pucks_map.GetTilesBlock(bounds);
        for (int i = 0; i < allTiles.Length; i++)
        {
            if (allTiles[i] != null && allTiles[i].name == "puck")
            {
                pucks++;
            }
        }
        return pucks;
    }

    static public float GetDefaultVelocity()
    {
        return default_velocity;
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
        Debug.Log(game_score);
    }

    public void ChangeScore(int amount)
    {
        
        SetScore(game_score + amount);
    }

    public void DeletePuckAt(Vector3Int position)
    {
        pucks_count--;
        Debug.Log(pucks_count);
        pucks_map.SetTile(position, null);
    }
}
