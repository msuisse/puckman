using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Class for all entities that can move throught the maze.
/// </summary>
public class MazeMover : MonoBehaviour
{
    /// <summary>Gets or sets the current target position </summary>
    /// <value>A Vector2 with the world coordonate of the current target</value>
    Vector2 target_pos;
    /// <summary>Gets or sets the entity velocity</summary>
    /// <value>A positive float for the entity movement speed.</value>
    public float velocity;
    /// <summary>Gets or sets the current tile distance tolerence when moving</summary>
    /// <value>A float for the minimum world distance between an entity and its target that will be equal to be on top of it.</value>
    private float _tile_tolerance = 0.01f;
    /// <summary>Gets or sets the current direction</summary>
    /// <value>A normalized Vector2 with only one direction. Default value is a null vector</value>
    Vector2 direction = Vector2.zero;


    public delegate void OnEnterTileDelegate();
    public OnEnterTileDelegate OnEnterTile;

    // Start is called before the first frame update
    void Start() {
        target_pos = transform.position;
    }

    // Update is called once per frame
    void Update() {

        UpdateTargetPosition();

        MoveToPosition();
    }

    /// <summary>
    /// Move the sprite to its new position for this update
    /// </summary>
    private void MoveToPosition()
    {
        float distance_for_update = velocity * Time.deltaTime;

        Vector2 target_distance = target_pos - (Vector2)transform.position;
        Vector2 move_for_update = target_distance.normalized * distance_for_update;

        UpdateAnimationDirection(move_for_update);        

        //Do not go past the target if we should move past it
        if (target_distance.SqrMagnitude() < move_for_update.SqrMagnitude())
        {
            //Just place the player at the target position
            transform.position = target_pos;
            return;
        }

        transform.Translate((Vector3)move_for_update);      
        
        if( Vector2.Distance(target_pos, transform.position) < _tile_tolerance)
        {
            transform.position = target_pos;
        }
    }

    /// <summary>
    /// Update the sprite to face the direction it is moving.
    /// </summary>
    /// <remarks>
    /// Given that our sprite have theirs pivots on the bottom left corner we have to offset the sprite if the direction is not to the right.
    /// Pacman and the ghost beeing the only sprite to update and beeing the same size we hardcode it here. A better solution would be to use the sprite size to do that
    /// </remarks>
    /// <param name="current_movement">The movement applied this update</param>
    private void UpdateAnimationDirection(Vector2 current_movement)
    {
        //Entering a tile let other script react to it
        if(OnEnterTile != null)
        {
            OnEnterTile();
        }

        //Update only if we are currently moving
        if (current_movement.SqrMagnitude() > 0)
        {
            if (Mathf.Abs(current_movement.x) > Mathf.Abs(current_movement.y))
            {
                // Move West
                if (current_movement.x < 0)
                {
                    // Little hack to face left: just change the scale to -1 on x will "flip" the sprite horizontaly
                    transform.GetChild(0).transform.localScale = new Vector3(-1, 1, 1);
                    transform.GetChild(0).transform.rotation = Quaternion.Euler(Vector3.zero);
                    transform.GetChild(0).transform.localPosition = new Vector3(1, 0, 0);
                }
                // Move East (Default position)
                else
                {
                    transform.GetChild(0).transform.localScale = Vector3.one;
                    transform.GetChild(0).transform.rotation = Quaternion.Euler(Vector3.zero);
                    transform.GetChild(0).transform.localPosition = Vector3.zero;
                }
            }
            else
            {
                // Move Souht
                if (current_movement.y < 0)
                {                    
                    transform.GetChild(0).transform.localScale = Vector3.one;
                    transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                    transform.GetChild(0).transform.localPosition = new Vector3(0, 1, 0);
                }
                // Move North
                else
                {
                    transform.GetChild(0).transform.localScale = Vector3.one;
                    transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    transform.GetChild(0).transform.localPosition = new Vector3(1, 0, 0);
                }

            }
        }
    }

    /// <summary>
    /// Try to update the current target to the set direction. If the move is not legal set the target to the current position.
    /// </summary>
    private void UpdateTargetPosition() {

        // Are we there Yet?
        float distance_to_target = Vector3.Distance(transform.position, target_pos);
        if (distance_to_target > 0)
        {
            // Not there yet, no need to update anything.
            return;
        }

        target_pos += direction;
        target_pos = FloorPosition(target_pos);

        if (IsLegalMove(target_pos)) {
            return;
        }

        target_pos = transform.position;
    }

    /// <summary>
    /// Check if the next move will be valid by checking the Tile at the current position + direction 
    /// </summary>    
    /// <param name="can_use_gate">Let the gate as a permitted tile to pass</param>
    /// <returns>True if the next move is legal</returns>
    /// TODO Clean up this mess and check if it has any real use!!!
    public Boolean IsNextMoveLegal(bool can_use_gate)
    {
        if (GameManager.walls_map.WorldToCell((Vector2)transform.position + direction) == GameManager.gate_position && can_use_gate)
        {
            return true;
        }
        return IsLegalMove( (Vector2)transform.position + direction);
    }

    /// <summary>
    /// Check that a position is actually "legal" e.g. is not a wall nor the ghosthouse gate
    /// </summary>
    /// <param name="pos"></param>
    /// <returns>True if the move is legal</returns>
    public Boolean IsLegalMove(Vector2 pos)
    {
        if (GameManager.walls_map.GetTile(GameManager.walls_map.WorldToCell(pos)) != null || GameManager.walls_map.WorldToCell(pos) == GameManager.gate_position)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    ///  Return the given vector after rounding  x and y using Mathf.FloorToInt()
    /// </summary>
    /// <param name="pos">A Vector2 to round down</param>
    /// <returns></returns>
    Vector2 FloorPosition(Vector2 pos)
    {
        return new Vector2(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y));
    }

    /// <summary>
    /// Set the new direction to go to.
    /// </summary>
    /// <param name="new_direction">A Vector2 normalized and with only 1 direction.</param>
    public void SetNewDirection(Vector2 new_direction) {
        Vector2 testPos = target_pos + new_direction;
        if (IsLegalMove(testPos) == false)
        {
            // Trying to move into a wall, ignore input.
            return;
        }
        direction = new_direction;
    }

    public Vector2 GetDirection() {
        return direction;
    }

    /// <summary>
    /// Reset the current target to the current position
    /// </summary>
    internal void ResetTarget()
    {
        target_pos = transform.position;
    }



}
