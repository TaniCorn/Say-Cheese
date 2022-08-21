using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class TempPlayerMovementFPS : MonoBehaviour
{

    private CharacterController characterController;
    private Transform groundCheck;
    [SerializeField]
    private float m_Speed = 30.0f;

    Vector3 velocity;
    public float gravity = -49.81f;

    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    bool isGrounded=false;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        groundCheck = GameObject.Find("Player_GroundCheck").transform;
    }

    // Update is called once per frame
    void Update()
    {
     
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * m_Speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }
}
