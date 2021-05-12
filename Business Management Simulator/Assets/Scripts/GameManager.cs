using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float time1;
    public float time2;
    public float QuestionAnswered;
    public Text NewQuestionArrived;
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        if (Time.time < 10)
        {
            PlayerPrefs.SetFloat("QuestionAnswered", 0);
        }
    }

    void Update()
    {
        if (Time.time > PlayerPrefs.GetFloat("QuestionAnswered") + 10)
        {
            NewQuestionArrived.text = "NewQuestionArrived";
        }
    }
}
