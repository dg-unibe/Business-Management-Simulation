using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuestionManager : MonoBehaviour
{
    public string[] question;
    public string[] AnswerA;
    public string[] AnswerB;
    public Text[] AnswerList;

    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    public TextMeshProUGUI QuestionText;
    public Button AnswerABtn;
    public Button AnswerBBtn;

    public Vector3 a;
    public Vector3 b;

    public float time1;
    public float time2 = 10;

    public float QuestionAnswered;

    public bool IsQuestionAnswered;

    public int count;

    
    private void Start()
    {
        if(Time.time < 10)
        {
            PlayerPrefs.SetFloat("QuestionAnswered",0);
        }

        QuestionAnswered = PlayerPrefs.GetFloat("QuestionAnswered");

        if (time1 > PlayerPrefs.GetFloat("QuestionAnswered") + 10)
        {
            ShowQuestion();
        }

        if (!PlayerPrefs.HasKey("Count"))
            count = 0;
        else
            count = PlayerPrefs.GetInt("Count");

    }

    void ShowQuestion()
    {
        panel1.SetActive(true);
        panel2.SetActive(true);
        panel3.SetActive(false);
        QuestionText.text = question[count];
        AnswerABtn.transform.GetChild(0).GetComponent<Text>().text = AnswerA[count];
        AnswerBBtn.transform.GetChild(0).GetComponent<Text>().text = AnswerB[count];

        int rand = Random.Range(0, 2);

        a = AnswerABtn.transform.position;
        b = AnswerBBtn.transform.position;
        if (rand == 0)
        {
            AnswerABtn.transform.position = a;
            AnswerBBtn.transform.position = b;
        }
        else
        {
            AnswerABtn.transform.position = b;
            AnswerBBtn.transform.position = a;
        }

    }
    void ShowList()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(true);

        for (int i = 0; i < 12; i++)
        {
            if (PlayerPrefs.GetInt("Answer" + i) == 0)
            {
                AnswerList[i].text = "Question " + i.ToString() + " :  " + " Wrong";
            }
            else if (PlayerPrefs.GetInt("Answer" + i) == 1)
            {
                AnswerList[i].text = "Question " + i.ToString() + " :  " + " Correct";
            }
        }

        PlayerPrefs.DeleteAll();
        count = 0;
        QuestionAnswered = 0;
        PlayerPrefs.SetFloat("QuestionAnswered", 0);
    }
    public void correctanswer()
    {
        print("correct");
        
        count = count + 1;
        PlayerPrefs.SetInt("Answer" + count, 1);
        PlayerPrefs.SetInt("Count", count);
        QuestionAnswered = Time.time;
        PlayerPrefs.SetFloat("QuestionAnswered", QuestionAnswered);
        IsQuestionAnswered = false;
        panel1.SetActive(false);
        panel2.SetActive(false);
    }
    public void wronganswer()
    {
        print("wrong");
       
        count = count + 1;
        PlayerPrefs.SetInt("Answer" + count, 0);
        PlayerPrefs.SetInt("Count", count);
        QuestionAnswered = Time.time;
        PlayerPrefs.SetFloat("QuestionAnswered", QuestionAnswered);
        IsQuestionAnswered = false;
        panel1.SetActive(false);
        panel2.SetActive(false);
    }

    private void Update()
    {
        time1 = Time.time;

        if (time1 > QuestionAnswered + 10)
        {
            if (IsQuestionAnswered == false)
            {
                if (count < 12)
                {
                    IsQuestionAnswered = true;
                    ShowQuestion();
                }
                else if (count >= 12)
                {
                    IsQuestionAnswered = true;
                    ShowList();
                }
            }
        }
    }
}
