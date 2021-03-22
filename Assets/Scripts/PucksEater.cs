using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PucksEater : MonoBehaviour
{
    GameManager game_manager;
    private GamePreloader game_preloader;    

    // Start is called before the first frame update
    void Start()
    {
        game_manager = GameObject.FindObjectOfType<GameManager>();
        game_preloader = GameObject.FindObjectOfType<GamePreloader>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        // When a pelletEater touch a pellet in the tilemap he eat it
        if (GameManager.pucks_map.GetTile(GameManager.pucks_map.WorldToCell(transform.position)) != null)
        {            
            // TODO: Find a better way to check what type of bonus was picked up 
            if(GameManager.pucks_map.GetSprite(GameManager.pucks_map.WorldToCell(transform.position)).name == "puck")
            {
                game_manager.ChangeScore(10);
            }
            else if (GameManager.pucks_map.GetSprite(GameManager.pucks_map.WorldToCell(transform.position)).name == "super_puck")
            {
                game_manager.ChangeScore(100);
                // Make the player immune to ghost for X second and increase his speed
            }
            else
            {
                //Every other case are fruit, for now we have a single value for all fruits
                game_manager.ChangeScore(200);
            }

            // Delete the tile content
            game_preloader.PlayMunch();
            game_manager.DeletePuckAt( (Vector3Int)GameManager.pucks_map.WorldToCell(transform.position));
        }
    }
}
