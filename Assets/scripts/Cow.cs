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
    [SerializeField] private AudioSource cowSoundSource;

    public Animator cowAnimator;

    [SerializeField]private const float eatingTimer = 2.0f;
    [SerializeField]private const float roamingTimer = 5.0f;
    private float timer;

    private GameObject shipAttacking;

    private Vector3 moveDirection;
    private Vector3 lookDirection;

    [SerializeField][Range(0,10.0f)] private float randomTimerAdditions = 5.0f;
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
                timer = roamingTimer + Random.Range(0, randomTimerAdditions);
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
                timer = eatingTimer + Random.Range(0, randomTimerAdditions); ;
                currState = GameState.Idle;
                return;
            }

            rb.MovePosition(transform.position + (moveDirection * speed * Time.deltaTime));
            transform.LookAt(this.transform.position + lookDirection);
            //Quaternion rot = Quaternion.LookRotation(new Vector3(0,0,1), Vector3.up);
            //rb.transform.rotation = rot;
        }
        else if (currState == GameState.Attacking)
        {
            if (!shipAttacking)
            {
                shipAttacking = null;
                AttackInterrupted();
                cowAnimator.SetBool("Eating", false);
                cowAnimator.SetBool("Roaming", true);
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
        PlayCowSound();
        //transform.Rotate(new Vector3(0, 1, 0), 45 * Time.deltaTime);
        transform.Rotate(new Vector3(1, 1, 0.7f), 200 * Time.deltaTime);
    }
    
    public void PlayCowSound()
    {
        if(cowSoundSource.isPlaying == false)
            cowSoundSource.Play();
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
                moveDirection = new Vector3(1, 0, 0);
                lookDirection = new Vector3(0, 0, -1);
                return;
            case 1:
                moveDirection = new Vector3(-1, 0, 0);
                lookDirection = new Vector3(0, 0, 1);
                return;
            case 2:
                moveDirection = new Vector3(0, 0, -1);
                lookDirection = new Vector3(-1, 0, 0);
                return;
            case 3:
                moveDirection = new Vector3(0, 0, 1);
                lookDirection = new Vector3(1, 0, 0);
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
