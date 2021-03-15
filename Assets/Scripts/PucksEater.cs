using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PucksEater : MonoBehaviour
{
    GameManager game_manager;
    // Start is called before the first frame update
    void Start()
    {
        game_manager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // When a pelletEater touch a pellet in the tilemap he eat it
        if (GameManager.pucks_map.GetTile(GameManager.pucks_map.WorldToCell(transform.position)) != null)
        {
            game_manager.ChangeScore(10);
            // Delete the tile content
            game_manager.DeletePuckAt( (Vector3Int)GameManager.pucks_map.WorldToCell(transform.position));
        }
    }
}
