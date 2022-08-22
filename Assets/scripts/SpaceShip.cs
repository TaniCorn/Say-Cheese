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
    [SerializeField] private float timer;
    [SerializeField] private Cow target;
    // Start is called before the first frame update
    void Start()
    {
        currState = GameState.Roaming;
        timer = 3000;
    }

    // Update is called once per frame

    public void TakeDamage() {
        life -= 500;    
    }
    void Update()
    {
        if (life <= 0)
        {
            currState = GameState.Dead;
        }

        if (timer <= 0 && target != null)
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
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * 100);
            if (transform.position == target.transform.position)
            {
                Destroy(target.gameObject);
                currState = GameState.Roaming;
                timer = 3000;
                captured++;
                target = null;
                FindNewTarget();
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

    private void FindNewTarget()
    {
        Cow[] cows = FindObjectsOfType<Cow>();

        if (cows.Length > 0)
        {
            int rand = Random.Range(0, cows.Length);
            target = cows[rand];
        }
;
    }
}
