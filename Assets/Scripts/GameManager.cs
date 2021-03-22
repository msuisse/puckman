using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>Gets or sets the pucks Tilemap</summary>
    /// <value>A Tilemap with the pucks</value>
    static public Tilemap pucks_map { get; set; }
    /// <summary>Gets or sets the walls Tilemap</summary>
    /// <value>A Tilemap with the walls</value>
    static public Tilemap walls_map;
    /// <summary>Gets or sets the tile where the player should start on the grid</summary>
    /// <value>a Vector3Int with the default starting position. Default is Vector3Int(-4, -9, 0)</value>
    static public Vector3Int player_start_position = new Vector3Int(-4, -9, 0);
    /// <summary>Gets or sets the tile where the ghosthouse's gate is on the grid</summary>
    /// <value>a Vector3Int with the ghosthouse's gate position. Default is Vector3Int(-4, -1, 0)</value>
    static public Vector3Int gate_position = new Vector3Int(-4, -1, 0);
    /// <summary>Time to wait between bonus spawns</summary>
    /// <value>Time in second to wait before spawning a new bonus</value>
    public int time_between_bonus = 20;

    /// <summary>
    /// An array of the differents bonus sprites. Set in editor.
    /// </summary>    
    public Sprite[] bonus_sprites;

    /// <summary>The text component to displaythe score in the scene.</summary>
    Text score_text;
    /// <summary>Gets or sets the default velocity for all entities</summary>    
    static private float default_velocity = 3f;

    /// <summary>Gets of Sets the game score</summary>
    /// <value>An integer of the current score. Default value is 0</value>
    private int game_score = 0;
    /// <summary>Gets of Sets the number of pucks still on the map</summary>
    /// <value>An integer of the current number of pucks left</value>
    private int pucks_count;
    /// <summary>Gets of Sets the time left before the next bonus</summary>
    /// <value>An float of the time left before we can spawn another bonus.</value>
    private float time_to_bonus;
    /// <summary>Gets of Sets the current level</summary>
    /// <value>An integer of the current level. Default is 1 </value>
    private int level_counter = 1;

    // Start is called before the first frame update
    void Start()
    {
        walls_map = GameObject.FindObjectOfType<WallsTilemap>().GetComponent<Tilemap>();
        pucks_map = GameObject.FindObjectOfType<PucksTilemap>().GetComponent<Tilemap>();
        initGameScores();
        score_text = GameObject.FindObjectOfType<Canvas>().GetComponentInChildren<Text>();

        pucks_count = InitPucksCounter();
        time_to_bonus = time_between_bonus;


    }

    // Update is called once per frame
    void Update()
    {
        CheckforBonus();

        // No more puck to eat, stage complete
        if (pucks_count == 0)
        {
            LevelFinished();
        }

    }

    /// <summary>
    /// Reset the level by deleting the finished pucksMap and adding a new one. Then reseting the player, enemies and puck counter
    /// </summary>
    private void LevelFinished()
    {
        //TODO Make everything stop on screen and wait a few second.

        // Replace the pucks map with a new one.
        Destroy(GameObject.FindObjectOfType<PucksTilemap>().gameObject);
        Instantiate(Resources.Load("Prefabs/PucksTilemap"), Vector3.zero, Quaternion.identity, GameObject.FindObjectOfType<Grid>().transform);
        pucks_map = GameObject.FindObjectOfType<PucksTilemap>().GetComponent<Tilemap>();

        //Reset the player
        GameObject.FindObjectOfType<PlayerMover>().ResetPlayer();

        //TODO Reset the enemies

        //Reset the puck counter for the new level.
        pucks_count = InitPucksCounter();

        //TODO New level should increase speed 

        level_counter++;
    }

    /// <summary>
    /// Checks if a new bonus should be spawned and do it if needed
    /// </summary>
    private void CheckforBonus()
    {
        time_to_bonus -= Time.deltaTime;
        if (time_to_bonus <= 0)
        {
            //Creating a tile from one of the sprites for bonus at rendom
            Tile new_bonus = ScriptableObject.CreateInstance<Tile>();
            new_bonus.sprite = (Sprite)bonus_sprites.GetValue(UnityEngine.Random.Range(0, bonus_sprites.Length));
            //The position for the bonus is fixed and the same as the one the player start.
            Vector3Int tile_pos = new Vector3Int(-4, -9, 0);
            pucks_map.SetTile(tile_pos, new_bonus);

            //Reseting the bonus system
            time_to_bonus = time_between_bonus;
        }
    }


    /// <summary>
    /// Delete the puck int the given tile.
    /// </summary>
    /// <param name="position">A Vector3Int of the grid position of the tile to delete (not the world position!)</param>
    public void DeletePuckAt(Vector3Int position)
    {
        pucks_count--;
        Debug.Log(pucks_count);
        pucks_map.SetTile(position, null);
    }

    /// <summary>
    /// Count all pucks on the map and return it.
    /// </summary>
    /// <returns>An int of the number of pucks on the map. Should alway be > 0 </returns>
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

    /// <summary>
    /// Gets the default velocity for the entity.
    /// </summary>
    /// <returns>A float of the default game velocity</returns>
    static public float GetDefaultVelocity()
    {
        return default_velocity;
    }

    /// <summary>
    /// Set the game score to 0
    /// </summary>
    private void initGameScores()
    {
        SetScore(0);
    }

    /// <summary>
    /// Return the current score.
    /// </summary>
    /// <returns>An int of the current score.</returns>
    public int GetScores()
    {
        return game_score;

    }

    /// <summary>
    /// Set the current score to the given value regardless of the score before calling it.
    /// </summary>
    /// <param name="value">The new score value</param>
    public void SetScore(int value)
    {
        game_score = value;
        score_text.text = "Score: " + game_score.ToString();
    }

    /// <summary>
    /// Change the current score by the given amount
    /// </summary>
    /// <param name="amount">The amount to change the score by. Can be less than 0 to remove points</param>
    public void ChangeScore(int amount)
    {

        SetScore(game_score + amount);
    }

}
