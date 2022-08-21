using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObject : MonoBehaviour
{
    [SerializeField]
    private float objectLifetime = 10.0f;
    public GameObject particleEffect;

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
        if (collision.gameObject.GetComponent<ThrownObject>())//Replace with spaceship script
        {
            //Make Spaceship take damage

            particleEffect.active = true;
            particleEffect.transform.parent = null;
        }
    }
}
