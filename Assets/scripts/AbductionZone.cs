using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbductionZone : MonoBehaviour
{

    [SerializeField] private AudioSource shipFinishedAbductionSource;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Cow>(out Cow cow))
        {
            CowCaptured(cow);
        }
    }

    public void CowCaptured(Cow cow)
    {
        Debug.LogWarning("Cow is Removed");
        GetComponentInParent<SpaceShip>().target = null;
        Destroy(cow.gameObject);
        manager.RemoveCow();
        shipFinishedAbductionSource.Play();
    }
}
