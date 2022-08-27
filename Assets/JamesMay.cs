using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamesMay : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ThrownObject>(out ThrownObject t))
        {
            GetComponent<AudioSource>().Play();

        }
    }
}
