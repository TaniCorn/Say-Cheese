using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallZoneFix : MonoBehaviour
{
    [SerializeField] private GameObject resetPosition;
    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.position = resetPosition.transform.position;
    }
}
