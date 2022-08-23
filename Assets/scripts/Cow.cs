using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    [SerializeField] private GameState currState;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int current = 0;

    [SerializeField] public float speed = 10;
    [SerializeField] public float rayDistance = 10;
    [SerializeField] private LayerMask shipMask;

    public Animator cowAnimator;

    [SerializeField]private const float eatingTimer = 2.0f;
    [SerializeField]private const float roamingTimer = 5.0f;
    private float timer;

    private GameObject shipAttacking;

    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        timer = roamingTimer;
        RandomDirection();
        currState = GameState.Roaming;
    }

    void Update()
    {
        if (CheckIfBeingAttacked())
        {
            Debug.Log("<color=Green>Cow Has Recognized Attacker</color>");
            
            currState = GameState.Attacking;
        }

        if (currState == GameState.Idle)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Debug.Log("<color=Green>Cow Going into Roaming Mode</color>");
                cowAnimator.SetBool("Roaming", true);
                cowAnimator.SetBool("Eating", false);
                timer = roamingTimer;
                currState = GameState.Roaming;
                RandomDirection();
            }
        }
        else if (currState == GameState.Roaming)
        {

            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Debug.Log("<color=Green>Cow Going into Idle Mode</color>");
                cowAnimator.SetBool("Eating", true);
                cowAnimator.SetBool("Roaming", false);
                timer = eatingTimer;
                currState = GameState.Idle;
                return;
            }

            rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
        }
        else if (currState == GameState.Attacking)
        {
            if (!shipAttacking)
            {
                shipAttacking = null;
                AttackInterrupted();
                cowAnimator.SetBool("Eating", true);
                cowAnimator.SetBool("Roaming", false);
                Debug.Log("<color=Green>Cow Going into Roaming Mode</color>");

            }
            BeingAttacked();
        }
        else if (currState == GameState.Retreating)
        {

        }
        else if (currState == GameState.Dead)
        {

        }
    }

    private bool CheckIfBeingAttacked()
    {
        if (Physics.Raycast(this.gameObject.transform.position ,Vector3.up, out RaycastHit hitinfo,rayDistance,shipMask))
        {
            if (hitinfo.collider.TryGetComponent<SpaceShip>(out SpaceShip spaceShip))
            {
                if (spaceShip.target == this.gameObject)
                {
                    shipAttacking = spaceShip.gameObject;
                    return true;
                }
            }
        }
        return false;
    }

    private void BeingAttacked()
    {
        //this.gameObject.transform.Rotate(new Vector3(90, 90, 0));
    }
    private void AttackInterrupted()
    {
        currState = GameState.Roaming;
    }
    private void RandomDirection()
    {
        float rand = Random.Range(0, 3);

        switch (rand)
        {
            case 0:
                direction = new Vector3(1, 0, 0);
                return;
            case 1:
                direction = new Vector3(-1, 0, 0);
                return;
            case 2:
                direction = new Vector3(0, 0, -1);
                return;
            case 3:
                direction = new Vector3(0, 0, 1);
                return;
            default:
                Debug.LogError("NO DIRECTION FOR COW");
                break;
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.gameObject.transform.position, this.gameObject.transform.position + Vector3.up * rayDistance);
    }

}
