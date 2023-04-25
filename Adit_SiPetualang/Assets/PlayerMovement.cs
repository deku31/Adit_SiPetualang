using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody Player;
    [SerializeField] private float gravitasi=10;
    [SerializeField] private float speed=10;
    [SerializeField] private float jumpForce=6;
    bool isGrounded;
    public LayerMask groundLayer;


    private void Awake()
    {
        if (Player == null)
        {
            Player=gameObject.GetComponent<Rigidbody>();
        }
    }
    void Start()
    {
        Player.mass = gravitasi;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        Vector3 MoveDir = new Vector3(x, 0, 0).normalized;


        // Flip the player object if moving left or right
        if (x < 0)
        {
            transform.localScale= new Vector3(1f, 1, -1f);
        }
        else if (x> 0)
        {
            transform.localScale = new Vector3(1f, 1, 1f);

        }
        Player.AddForce(MoveDir * speed);
        // Apply the movement velocity to the Rigidbody
        Player.velocity = MoveDir* speed+ new Vector3(0f, Player.velocity.y, 0f);

        // Check if the player is grounded
        isGrounded = Physics.Raycast(transform.position, -Vector3.up,groundLayer);
        Debug.Log(isGrounded);
        // Check if the player presses the jump button and is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded==false)
        {
            // Apply a vertical force to the Rigidbody to simulate a jump
            Player.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
