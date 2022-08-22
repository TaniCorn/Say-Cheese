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
   
    [SerializeField] private GameState currState;
    [SerializeField] private Vector3[] roamPoints;
    [SerializeField] private int current = 0;
    [SerializeField] private int life = 3000;
    [SerializeField] private int captured = 0;
    [SerializeField] private GameObject cow;
    [SerializeField] private float timer;
    // Start is called before the first frame update
    void Start()
    {
        currState = GameState.Roaming;
        timer = 3000;
    }

    // Update is called once per frame

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "cheese")
        {
            life -= 100;
        }
    }
    void Update()
    {
        if (life < 0)
        {
            currState = GameState.Dead;
        }

        if (timer <= 0 && cow != null)
        {
            currState = GameState.Attacking;
        }

        if (currState == GameState.Idle)
        {
           
        }
        else if (currState == GameState.Roaming)
        {
            transform.position = Vector3.MoveTowards(transform.position, roamPoints[current], Time.deltaTime * 200);
            if(transform.position == roamPoints[current])
            {
                current = Random.Range(0, roamPoints.Length);
            }
            timer--;
        }
        else if (currState == GameState.Attacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, cow.GetComponentInChildren<Transform>().position, Time.deltaTime * 100);
            if (transform.position == cow.GetComponentInChildren<Transform>().position)
            {
                Destroy(cow.transform.GetChild(0).gameObject);
                currState = GameState.Roaming;
                timer = 3000;
                captured++;
                
            }
        }
        else if(currState == GameState.Retreating)
        {
            //if damaged below x% begin to retreat away off the map and despawn
        }
        else if (currState == GameState.Dead)
        {
            Destroy(gameObject);
            //despawn
        }
    }
}
