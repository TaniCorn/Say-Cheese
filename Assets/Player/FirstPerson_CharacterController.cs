using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPerson_CharacterController : MonoBehaviour
{
    [SerializeField][Tooltip("")]
    public Camera _camera;
    private Rigidbody _rigidbody;

    [SerializeField]
    private float mouseSensitivity = 300f;
    [SerializeField]
    private float speed = 20.0f;

    private float xRotation = 0f;

    private Vector3 playerMovement;

    [SerializeField] private float headHeight = 2.0f;
    bool locked = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _camera.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + headHeight, transform.position.z);
        _camera.gameObject.transform.rotation = transform.rotation;

        _camera.gameObject.transform.parent = transform;

        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCameraControl();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                locked = false;
            }
            else
            {
                locked = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

            }
        }
        if (Input.GetMouseButtonDown(0) && !locked)
        {
            locked = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }
    private void FixedUpdate()
    {
        PlayerMovementControl();

    }
    private void PlayerCameraControl()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        _camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void PlayerMovementControl()
    {
        playerMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 move = (transform.right * playerMovement.x + transform.forward * playerMovement.z);
        //Projects a sphere underneath player to check ground layer

        //Player recieves a constant y velocity from gravity
        //playerFallingVelocity.y += playerGravity;// * Time.deltaTime;

        //If player is fully grounded then apply some velocity down, this will change the 'floating' period before plummeting.
        //if (isGrounded && playerFallingVelocity.y < 0)
        //{
        //    playerFallingVelocity.y = -5f;
        //}
        _rigidbody.MovePosition(transform.position + (move * speed * Time.deltaTime));
        //move.y = _rigidbody.velocity.y;
        //_rigidbody.velocity = move * Time.deltaTime;
        
        
    }
}
