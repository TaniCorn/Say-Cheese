using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObject : MonoBehaviour
{
    [SerializeField]
    private float objectLifetime = 10.0f;
    public GameObject particleEffect;
    bool played = false;
    int timesPlayed = 5;
    public ParticleSystem PA_Collision;

    public SpaceShip mostRecentSpaceshipDamaged;

    public void FixedUpdate()
    {
        if (!GetComponent<BoxCollider>().enabled)
            return;

        objectLifetime -= Time.deltaTime;
        if (objectLifetime <= 0)
        {
            Destroy(particleEffect);
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        timesPlayed--;
        PA_Collision.Emit(timesPlayed);
        //if (!played)
            //PA_Collision.Play();
        //played = true;
        if (collision.gameObject.TryGetComponent<SpaceShip>(out SpaceShip ship))//Replace with spaceship script
        {
            //Spaceship can't be damaged twice
            if (ship == mostRecentSpaceshipDamaged)
            {
                return;
            }
            mostRecentSpaceshipDamaged = ship;
            
            //Make Spaceship take damage
            ship.TakeDamage();

            //Particle Effect
            particleEffect.active = true;
            particleEffect.transform.parent = null;
        }

        if (collision.gameObject.TryGetComponent<Cow>(out Cow cow))//Replace with spaceship script
        {
            //Make Cow MOO
            cow.PlayCowSound();

        }
    }
}
