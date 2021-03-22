using UnityEngine;

public class ChaseRandom : MonoBehaviour, IChaseBehavior
{
    public ChaseRandom()
    {
        
    }

    public Vector2 ChooseDirection(MazeMover maze_mover, bool can_use_gate)
    {
        Vector3 current_direction = maze_mover.GetDirection();
        Vector2 newDir = Vector2.zero;
        
        // Are we going into a wall ?
        if ( !maze_mover.IsNextMoveLegal(can_use_gate) )
        {
            newDir = maze_mover.GetDirection();
            newDir.x *= -1f;
            newDir.y *= -1f;
            maze_mover.SetNewDirection(newDir);
        } 
        
        //Do we continue straight ?
        if (Random.Range(0f, 1f) < 0.5)
        {
            return maze_mover.GetDirection();
        }
        //We change direction
        //Are we moving left/right ? THen go up or down and vice versa
        if (Mathf.Abs(current_direction.x) > 0)
        {
            newDir.y = Random.Range(0, 2) == 0 ? -1 : 1;
        } 
        else
        {
            newDir.x = Random.Range(0, 2) == 0 ? -1 : 1;
        }
        
        return newDir;
    }
}
