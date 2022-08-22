using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObject : MonoBehaviour
{
    [SerializeField]
    private float objectLifetime = 10.0f;
    public GameObject particleEffect;

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
    }
}
