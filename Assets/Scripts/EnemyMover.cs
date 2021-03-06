using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public Vector2 Starting_position;

    MazeMover maze_mover;
    bool can_use_gate;

    private IChaseBehavior chase_behavior;
    List<MonoBehaviour> chase_behaviors = new List<MonoBehaviour>();
    private PlayerMover player;

    // Start is called before the first frame update
    void Start()
    {
        maze_mover = GetComponent<MazeMover>();
        maze_mover.OnEnterTile += OnEnterTile;
        maze_mover.velocity = GameManager.GetDefaultVelocity()-1;
        
        
        // Retrieve the wanted chase behavior added in the editor
        CheckChaseBehavior();
                
        //TODO uncomment
        //can_use_gate = true;
        // For test until ghost are in the starting cell and have a behavior to get out of it
        can_use_gate = false;
    }

    // Update is called once per frame
    void Update()
    {   
    }

    private void CheckChaseBehavior()
    {
        var all_scripts = GetComponents<MonoBehaviour>();
        foreach (var item in all_scripts)
        {
            if (item is IChaseBehavior)
            {
                chase_behavior = (IChaseBehavior)item;
                break;
            }
        }
        if(chase_behavior == null)
        {
            throw new System.Exception("No chase behavior found on gameObject named: "+this.name);
        }
    }

    public void ResetToStartingPos()
    {
        transform.GetChild(0).transform.localScale = Vector3.one;
        transform.GetChild(0).transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.GetChild(0).transform.localPosition = Vector3.zero;
        transform.position = Starting_position;
        maze_mover.SetNewDirection(Vector2.zero);
        maze_mover.ResetTarget();
    }

    void OnEnterTile()
    {

        if (can_use_gate && transform.position == new Vector3(-4, 0, 0))
        {
            Debug.Log("No more gate!");
            can_use_gate = false;

        }
        
        Vector2 new_dir = chase_behavior.ChooseDirection(maze_mover, can_use_gate);
                
        maze_mover.SetNewDirection(new_dir);

    }
}
