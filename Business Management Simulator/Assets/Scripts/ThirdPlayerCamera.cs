using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPlayerCamera : MonoBehaviour
{
float rotationSpeed = 1;
    public Transform Target, Player;
    float mouseX, mouseY;

    
    void Start()
    {
    }

    private void LateUpdate()
    {
        CamControl();
    }
    

    void CamControl()
    {
        if(Time.deltaTime == 0)
        {
        return;
        }
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(Target);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else
        {
            Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            Player.rotation = Quaternion.Euler(0, mouseX, 0);
        }
    }

}
