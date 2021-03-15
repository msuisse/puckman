using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PucksEater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // When a pelletEater touch a pellet in the tilemap he eat it
        if (GameManager.pucks_map.GetTile(GameManager.pucks_map.WorldToCell(transform.position)) != null)
        {            
            // Delete the tile content
            GameManager.DeletePuckAt( (Vector3Int)GameManager.pucks_map.WorldToCell(transform.position));
        }
    }
}
