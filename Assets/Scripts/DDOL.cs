using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour
{
    //Dummy class to prevent the main gameobject to be reloaded
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
