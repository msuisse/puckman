using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    MazeMover mazeMover;
    // Start is called before the first frame update
    void Start()
    {
        mazeMover = GetComponent<MazeMover>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 player_direction = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
            );

        if (player_direction.SqrMagnitude() < 0.05f)
        {
            // The input is REALLY small (probably zero),
            // so don't change the desired direction
            return;
        }

        //Only Vertical or Horital movements allowed so with retrict it
        if (Mathf.Abs(player_direction.x) >= Mathf.Abs(player_direction.y))
        {
            player_direction.y = 0;
        } else {
            player_direction.x = 0;
        }

        mazeMover.SetNewDirection(player_direction.normalized);
    }
}
