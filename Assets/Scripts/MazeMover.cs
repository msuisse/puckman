using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MazeMover : MonoBehaviour
{

    float velocity = 0.3f;
    Vector2 direction = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        direction.x = 1 * velocity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3)direction);
        
    }
}
