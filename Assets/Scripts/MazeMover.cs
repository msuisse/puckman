using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeMover : MonoBehaviour
{
    Vector2 target_pos;
    float velocity = 3f;
    float _tile_tolerance = 0.01f;

    Vector2 direction = Vector2.zero;

    // Start is called before the first frame update
    void Start() {
        target_pos = transform.position;
        velocity = GameManager.GetDefaultVelocity();
    }

    // Update is called once per frame
    void Update() {

        UpdateTargetPosition();

        MoveToPosition();
    }

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

    private void UpdateAnimationDirection(Vector2 current_movement)
    {
        //Update only if we are currently moving
        if (current_movement.SqrMagnitude() > 0)
        {
            if (Mathf.Abs(current_movement.x) > Mathf.Abs(current_movement.y))
            {
                // Move West
                if (current_movement.x < 0)
                {
                    transform.GetChild(0).transform.localScale = new Vector3(-1, 1, 1);
                    transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    transform.GetChild(0).transform.localPosition = new Vector3(1, 0, 0);
                }
                // Move East
                else
                {
                    transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
                    transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 0);
                }
            }
            else
            {
                // Move Souht
                if (current_movement.y < 0)
                {                    
                    transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
                    transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                    transform.GetChild(0).transform.localPosition = new Vector3(0, 1, 0);
                }
                // Move North
                else
                {
                    transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
                    transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    transform.GetChild(0).transform.localPosition = new Vector3(1, 0, 0);
                }

            }
        }
    }

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

    Boolean IsLegalMove(Vector2 pos)
    {

        //Debug.Log("pos " + pos);
        //Debug.Log(GameManager.walls_map.WorldToCell(pos));
        //Debug.Log(walls_map.GetTile(walls_map.WorldToCell(pos)));

        if (GameManager.walls_map.GetTile(GameManager.walls_map.WorldToCell(pos)) != null)
        {
            return false;
        }
        return true;
    }

    Vector2 FloorPosition(Vector2 pos)
    {
        return new Vector2(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y));
    }

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


}
