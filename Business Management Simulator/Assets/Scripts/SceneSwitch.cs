using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(3);
        }
        else if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            SceneManager.LoadScene(1);
        }
        
    }
}
