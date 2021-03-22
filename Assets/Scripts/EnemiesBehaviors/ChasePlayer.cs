using UnityEngine;

public class ChasePlayer : MonoBehaviour, IChaseBehavior
{
    public Vector2 ChooseDirection(MazeMover maze_mover, bool can_use_gate)
    {
        Vector2 player_direction = GameObject.FindObjectOfType<PlayerMover>().GetComponent<MazeMover>().GetDirection();
        Vector2 player_pos = GameObject.FindObjectOfType<PlayerMover>().transform.position;
        Vector2 new_dir = Vector2.zero;
        Vector2 last_dir = maze_mover.GetDirection();

        Vector2 aaa = player_pos - (Vector2)transform.position;


        if (aaa.x > aaa.y)
        {
            if (player_pos.x > maze_mover.transform.position.x)
            {
                new_dir.x = 1;
            }
            else
            {
                new_dir.x = -1;
            }
        }
        else
        {
            if (player_pos.y > maze_mover.transform.position.y)
            {
                new_dir.y = 1;
            }
            else
            {
                new_dir.y = -1;
            }
        }
        Debug.Log(last_dir+" / "+ new_dir);
        Debug.Log(player_pos - (Vector2)transform.position);
        if(Vector2.Dot(last_dir, new_dir) < 0)
        {
            new_dir = last_dir;
        }

        if(!maze_mover.IsLegalMove((Vector2)transform.position + new_dir))
        {
            if(Mathf.Abs(new_dir.x) > 0)
            {
                new_dir.x = 0;
                new_dir.y = Random.Range(0, 2) == 0 ? -1 : 1;
            }
            else
            {
                new_dir.x = Random.Range(0, 2) == 0 ? -1 : 1;
                new_dir.y = 0;
            }
        }
        
        //if (player_pos.x > maze_mover.transform.position.x)
        //{
        //    new_dir.x = 1;
        //}
        //else
        //{
        //    new_dir.x = -1;
        //}
        //if (!maze_mover.IsLegalMove((Vector2)maze_mover.transform.position + new_dir))
        //{
        //    new_dir.x = 0;
        //}

        //if (new_dir.x == 0)
        //{
        //    if (player_pos.y > maze_mover.transform.position.y)
        //    {
        //        new_dir.y = 1;
        //    }
        //    else
        //    {
        //        new_dir.y = -1;
        //    }
        //}
        return new_dir;
    }
}

