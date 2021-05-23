using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Month : MonoBehaviour
{
    public TextMeshProUGUI month;
    public string[] Months;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Count"))
        {
            month.text = 1.ToString();
        }
        else
            month.text = PlayerPrefs.GetInt("Count").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
