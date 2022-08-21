using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Idle = 0,
    Roaming,
    Attacking,
    Retreating,
    Dead
}
public class SpaceShip : MonoBehaviour
{
    Vector3 roamPath;
    GameState currState;
    // Start is called before the first frame update
    void Start()
    {
        roamPath = new Vector3(0.02f, 0.0f, -0.02f);
        currState = GameState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currState == GameState.Idle)
        {
            transform.Translate(roamPath);
        }
        else if (currState == GameState.Roaming)
        {
            //apply the roampath variables
        }
        else if (currState == GameState.Attacking)
        {
            //cancel the roam path variables and attack
        }
        else if(currState == GameState.Retreating)
        {
            //if damaged below x% begin to retreat away off the map and despawn
        }
        else if (currState != GameState.Dead)
        {
            //despawn
        }
    }
}
