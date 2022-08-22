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
    [SerializeField] static private Vector3[] roamPoints;
    [SerializeField] private int current = 0;
    [SerializeField] private int life = 3000;
    [SerializeField] private int captured = 0;
    [SerializeField] private float timer;
    [SerializeField] private GameObject target;

    [SerializeField] public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        currState = GameState.Roaming;
        timer = 10;
    }

    public static void SetRoamPoints(Vector3[] rp)
    {
        roamPoints = rp;
    }
    // Update is called once per frame

    public void TakeDamage() {
        Debug.Log("<color=Blue>SpaceShip took damage</color>");
        life -= 500;
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0 && currState != GameState.Attacking)
        {
            currState = GameState.Attacking;
            FindNewTarget();
            Debug.Log("<color=Blue>SpaceShip is Attacking</color>");
        }
        if (life <= 0)
        {
            Debug.Log("<color=Blue>SpaceShip is Dead</color>");
            currState = GameState.Dead;
        }

        if (currState == GameState.Idle)
        {
           
        }
        else if (currState == GameState.Roaming)
        {
            transform.position = Vector3.MoveTowards(transform.position, roamPoints[current], Time.deltaTime * speed);
            if(transform.position == roamPoints[current])
            {
                current = Random.Range(0, roamPoints.Length);
            }
        }
        else if (currState == GameState.Attacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed/2);
            if (transform.position == target.transform.position)
            {
                Destroy(target.gameObject);
                Debug.Log("<color=Blue>SpaceShip is Roaming</color>");
                currState = GameState.Roaming;
                timer = 10;
                captured++;
                target = null;
                manager.RemoveCow();
            }
        }
        else if(currState == GameState.Retreating)
        {
            //if damaged below x% begin to retreat away off the map and despawn
        }
        else if (currState == GameState.Dead)
        {
            FindObjectOfType<manager>().RemoveShip();
            Destroy(gameObject);
            //despawn
        }
    }

    private void FindNewTarget()
    {
        Cow[] cows = FindObjectsOfType<Cow>();

        if (cows.Length > 0)
        {
            Debug.Log("<color=Blue>SpaceShip has Found Target</color>");
            int rand = Random.Range(0, cows.Length);
            target = cows[rand].gameObject;
        }
;
    }
}
