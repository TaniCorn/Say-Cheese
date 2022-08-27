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
   
    private GameState currState;//Current State

    static private Vector3[] roamPoints;//Our roaming points
    private int current = 0;//Our current Roam Point Index
    private float timer;//Time for roaming

    [HideInInspector] public GameObject target;//Cow Target
    private bool abducting;

    [SerializeField][Tooltip("The child object that contains the particle effect")]private GameObject _abductionEffect;

    [HideInInspector][Tooltip("The speed of the ship")] public float speed = 10;
    [SerializeField] private int life = 3000;//Total Life
    [SerializeField][Tooltip("Height the ship will float above the cow while abducting")] private Vector3 heightAboveCow = new Vector3(0,10,0);
    [SerializeField] private AudioSource shipAbductSoundSource;
    [SerializeField] private AudioSource shipHitSoundSource;
    private SoundJukebox jukebox;
    
    public static void SetRoamPoints(Vector3[] rp){roamPoints = rp;}
    public void TakeDamage(){Debug.Log("<color=Blue>SpaceShip took damage</color>");life -= 500; PlayHitSound(); }

    void Start()
    {
        jukebox = FindObjectOfType<SoundJukebox>();
        jukebox.AddUFO(this.gameObject);
        currState = GameState.Roaming;
        abducting = false;
        timer = 10;
    }


    void Update()
    {
        timer -= Time.deltaTime;

        //Change state to Attacking
        if (timer < 0 && currState != GameState.Attacking)
        {
            currState = GameState.Attacking;
            FindNewTarget();
            Debug.Log("<color=Blue>SpaceShip is Attacking</color>");
        }
        //Change state to Dead
        if (life <= 0)
        {
            jukebox.RemoveUFO(this.gameObject);
            PlayDieSound();
            Debug.Log("<color=Blue>SpaceShip is Dead</color>");
            currState = GameState.Dead;
        }


        #region STATE_MACHINE_
        //Ship is never:
            //Idle
            //Retreating

        if (currState == GameState.Roaming)
        {
            transform.position = Vector3.MoveTowards(transform.position, roamPoints[current], Time.deltaTime * speed);
            if(transform.position == roamPoints[current])
            {
                current = Random.Range(0, roamPoints.Length);
            }
        }
        else if (currState == GameState.Attacking)
        {
            if (target)
            {
                if (transform.position == target.transform.position + heightAboveCow || abducting)
                {
                    Debug.Log("<color=Blue>SpaceShip is collecting the Cow</color>");
                    AbductCow();
                    return;

                }
            }
            else
            {
                CowCaptured();
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, target.transform.position + heightAboveCow, Time.deltaTime * speed/2);

        }
        else if (currState == GameState.Dead)
        {
            if (target)
            {
                target.GetComponent<Rigidbody>().useGravity = true;

            }
            FindObjectOfType<manager>().RemoveShip();
            Destroy(gameObject);
            //despawn
        }
        #endregion
    }

    /// <summary>
    /// Finds any random cow and starts to target it.
    /// </summary>
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
    private void AbductCow()
    {

        //When the collider hits, the cow will get destroyed
        GetComponentInChildren<BoxCollider>().enabled = true;
        _abductionEffect.SetActive(true);
        if (!abducting)
        {
            _abductionEffect.transform.position = target.transform.position;
            _abductionEffect.GetComponent<ParticleSystem>().Play();
        }
        SetAbducting(true);
        PlayAbductSound();
        target.GetComponent<Rigidbody>().useGravity = false;
        
        Vector3 diff = target.transform.position - GetComponentInChildren<AbductionZone>().transform.position;
        Debug.LogWarning(diff.normalized);
        target.transform.position -= diff.normalized * Time.deltaTime ;
    }

    private void CowCaptured()
    {
        Debug.Log("<color=Blue>SpaceShip is Roaming</color>");
        GetComponentInChildren<BoxCollider>().enabled = false;
        _abductionEffect.SetActive(false);
        SetAbducting(false);
        target = null;
        currState = GameState.Roaming;
        timer = 10;
    }


    private void PlayAbductSound()
    {
        if(shipAbductSoundSource.isPlaying == false)
            shipAbductSoundSource.Play();
    }

    private void StopAbductSound()
    {
        shipAbductSoundSource.Stop();
    }

    private void PlayDieSound() { jukebox.PlayUFODeathSound();}
    
    private void PlayHitSound() {shipHitSoundSource.Play();}

    private void SetAbducting(bool value)
    {
        abducting = value;
        if(value == false)
            StopAbductSound();
    }
    
}
