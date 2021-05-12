using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;

    public float Gravity = 9.87f;
    private float verticalSpeed = 0;
    public float Speed = 3;

	void Update ()
    {
        Move();
    }

    private void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if(characterController.isGrounded)
        {
            verticalSpeed = 0;
        }
        else
        {
            verticalSpeed -= Gravity * Time.deltaTime;
        }
        
        Vector3 gravitymove = new Vector3(0, verticalSpeed, 0);
        Vector3 move = transform.forward * verticalMove + transform.right * horizontalMove;
        characterController.Move(Speed * Time.deltaTime * move + gravitymove * Time.deltaTime);
    }

}
