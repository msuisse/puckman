using UnityEngine;

public class ChasePlayerDirection : MonoBehaviour, IChaseBehavior
{


    public Vector2 ChooseDirection(MazeMover maze_mover, bool can_use_gate)
    {
        Vector2 player_direction = GameObject.FindObjectOfType<PlayerMover>().GetComponent<MazeMover>().GetDirection();
        Vector2 new_dir = Vector2.zero;
        
        if( Mathf.Abs(player_direction.x) > 0)
        {
            new_dir.x = player_direction.x;
            if (!maze_mover.IsLegalMove((Vector2)maze_mover.transform.position + new_dir) )
            {
                new_dir.x = 0;
                new_dir.y = Random.Range(0, 2) == 0 ? 1 : -1;
            }
        }
        else
        {
            new_dir.y = player_direction.y;
            if (!maze_mover.IsLegalMove((Vector2)maze_mover.transform.position + new_dir))
            {
                new_dir.y = 0;
                new_dir.x = Random.Range(0, 2) == 0 ? 1 : -1;
            }
        }
        return new_dir;
    }
}

