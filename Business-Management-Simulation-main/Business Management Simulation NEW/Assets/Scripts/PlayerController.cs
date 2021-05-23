using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public Animator anim;
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
        if (verticalMove != 0 || horizontalMove != 0)
            anim.SetBool("Run", true);
        else
            anim.SetBool("Run", false);
        Vector3 gravitymove = new Vector3(0, verticalSpeed, 0);
        Vector3 move = transform.forward * verticalMove + transform.right * horizontalMove;
        characterController.Move(-Speed * Time.deltaTime * move + gravitymove * Time.deltaTime);
        transform.GetChild(0).rotation = Quaternion.LookRotation(new Vector3(move.x, 0, move.z));
    }

}
