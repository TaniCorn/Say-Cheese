using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbductionZone : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Cow>(out Cow cow))
        {
            Debug.LogWarning("Cow is Removed");
            GetComponentInParent<SpaceShip>().target = null;
            Destroy(cow.gameObject);
            manager.RemoveCow();

        }
    }
}
