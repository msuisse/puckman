
using UnityEngine;


public interface IChaseBehavior 
{
    Vector2 ChooseDirection(MazeMover maze_mover, bool can_use_gate);
}