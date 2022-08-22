using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson_ThrowObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _hand;
    [SerializeField]
    private GameObject objectToThrow;
    [SerializeField]
    private GameObject objectInHand;

    [SerializeField]
    private float throwStrength = 2.0f;

    private bool reloading = false;
    private bool throwing = false;

    // Start is called before the first frame update
    void Start()
    {
        Transform camera = FindObjectOfType<FirstPerson_CharacterController>()._camera.transform;
        _hand.transform.parent = camera;
        _hand.transform.position = camera.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (reloading)
                return;
            if (objectInHand == null)
            {
                StartCoroutine("Reload");
                return;
            }

                StartCoroutine("Throwing");
            //Throw();
        }
    }

    private void Throw()
    {
        Debug.Log("Throw");
        objectInHand.AddComponent<Rigidbody>();
        objectInHand.GetComponent<BoxCollider>().enabled = true;
        objectInHand.transform.SetParent(null, true);
        objectInHand.GetComponent<Rigidbody>().AddForce((FindObjectOfType<FirstPerson_CharacterController>()._camera.transform.forward * throwStrength),ForceMode.Impulse);
        objectInHand = null;
        StartCoroutine("Reload");
    }
    IEnumerator Throwing()
    {
        objectInHand.transform.position -= FindObjectOfType<FirstPerson_CharacterController>()._camera.transform.forward;
        yield return new WaitForFixedUpdate();
        Throw();
    }
    private void NewThrowingObject()
    {
        Debug.Log("Reloaded");
        objectInHand = Instantiate(objectToThrow, _hand.transform);
    }

    IEnumerator Reload()
    {
        Debug.Log("Reloading");
        reloading = true;
        yield return new WaitForSeconds(1);
        NewThrowingObject();
        reloading = false;
    }

}
