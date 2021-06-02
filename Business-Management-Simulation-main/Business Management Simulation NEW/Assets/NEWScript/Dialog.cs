using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public string[] sentences;
    public bool[] isQuestion;
    public string[] answers1;
    public string[] answers2;
    
    public string[] feedbackAnswer1;
    public string[] feedbackAnswer2;


    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject answer1Button;
    public GameObject answer2Button;

    public GameObject MaleSpeechBubble;
    public GameObject FemaleSpeechBubble;

    public Animator MaleSpeaker;
    public Animator FemaleSpeaker;

    public AudioSource MaleTalkingAudio;
    public AudioSource FemaleThinkingAudio;
    public AudioSource FemaleTalkingAudio;
    public AudioSource FemaleCorrectAnswerAudio;
    public AudioSource FemaleWrongAnswerAudio;
    public AudioSource MaleFeedbackAudio;
    public AudioSource CorrectMusicAudio;
    public AudioSource WrongMusicAudio;
    public AudioSource ButtonClickAudio;

    public int hasAnswered = 0;// no, 1  answer1, 2 answer2

    private string prevSentence;

    void Start()
    {
        StartCoroutine(TypeMale());
        MaleSpeechBubble.SetActive(true);
        FemaleSpeaker.SetTrigger("startListening");
        MaleSpeaker.SetTrigger("startAsking");
    } 
    
    void Update()
    {
        if(MaleSpeechBubble.GetComponentInChildren<TMPro.TMP_Text>().text == sentences[index])
        {
            if(!isQuestion[index])
            {
                continueButton.SetActive(true);
            }
            else
            {
                answer1Button.GetComponentInChildren<TMPro.TMP_Text>().text = answers1[index];
                answer2Button.GetComponentInChildren<TMPro.TMP_Text>().text = answers2[index];
                if(hasAnswered == 0)
                {
                    MaleTalkingAudio.Stop();
                    answer1Button.SetActive(true);
                    answer2Button.SetActive(true);
                    //FemaleSpeaker.SetTrigger("startThinking");
                }
            }
            if(MaleSpeechBubble.GetComponentInChildren<TMPro.TMP_Text>().text != prevSentence)
            {
                MaleTalkingAudio.Stop();
                MaleSpeaker.SetTrigger("startListening");
                if(isQuestion[index]/* && !FemaleSpeaker.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Thinking")*/)
                {
                    FemaleSpeaker.SetTrigger("startThinking");
                    FemaleThinkingAudio.Play();
                }
            }
        }
        prevSentence = MaleSpeechBubble.GetComponentInChildren<TMPro.TMP_Text>().text;
    } 
    
    public void answer1Pressed()
    {
        //feedback.GetComponent<TMPro.TMP_Text>().text = feedbackAnswer1[index];
        //feedback.SetActive(true);
        ButtonClickAudio.Play();
        FemaleThinkingAudio.Stop();
        MaleSpeechBubble.SetActive(false);
        answer1Button.SetActive(false);
        answer2Button.SetActive(false);
        FemaleSpeechBubble.SetActive(true);
        StartCoroutine(TypeFemale1());
        //FemaleSpeechBubble.GetComponentInChildren<TMPro.TMP_Text>().text = answers1[index];

        continueButton.SetActive(true);
        hasAnswered = 1;

        //MaleSpeaker.SetTrigger("feedback1");
        FemaleSpeaker.SetTrigger("startTalking");
        FemaleTalkingAudio.Play();
    }
    
    public void answer2Pressed()
    {
        //feedback.GetComponent<TMPro.TMP_Text>().text = feedbackAnswer2[index];
        //feedback.SetActive(true);
        ButtonClickAudio.Play();
        FemaleThinkingAudio.Stop();
        MaleSpeechBubble.SetActive(false);
        answer1Button.SetActive(false);
        answer2Button.SetActive(false);
        FemaleSpeechBubble.SetActive(true);
        StartCoroutine(TypeFemale2());
        //FemaleSpeechBubble.GetComponentInChildren<TMPro.TMP_Text>().text = answers2[index];
        continueButton.SetActive(true);
        hasAnswered = 2;
        
        //MaleSpeaker.SetTrigger("feedback2");
        FemaleSpeaker.SetTrigger("startTalking");
        FemaleTalkingAudio.Play();
    }

    IEnumerator TypeMale(){
        
        MaleFeedbackAudio.Stop();
        MaleTalkingAudio.Play();
        MaleSpeechBubble.GetComponentInChildren<TMPro.TMP_Text>().text = "";
        foreach(char letter in sentences[index].ToCharArray())
        {
            MaleSpeechBubble.GetComponentInChildren<TMPro.TMP_Text>().text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator TypeFemale1(){
        
        FemaleSpeechBubble.GetComponentInChildren<TMPro.TMP_Text>().text = "";
        foreach(char letter in answers1[index].ToCharArray())
        {
            FemaleSpeechBubble.GetComponentInChildren<TMPro.TMP_Text>().text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator TypeFemale2(){
        
        FemaleSpeechBubble.GetComponentInChildren<TMPro.TMP_Text>().text = "";
        foreach(char letter in answers2[index].ToCharArray())
        {
            FemaleSpeechBubble.GetComponentInChildren<TMPro.TMP_Text>().text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        ButtonClickAudio.Play();
        continueButton.SetActive(false);
        if(hasAnswered == 1)//clicking continue after female answers 1
        {
            MaleSpeechBubble.GetComponentInChildren<TMPro.TMP_Text>().text = feedbackAnswer1[index];
            MaleSpeechBubble.SetActive(true);
            continueButton.SetActive(true);
            MaleSpeaker.SetTrigger("feedback1");
            FemaleSpeaker.SetTrigger("feedback1");
            CorrectMusicAudio.Play();
            MaleFeedbackAudio.Play();
            FemaleCorrectAnswerAudio.Play();
            FemaleTalkingAudio.Stop();
        }
        else if(hasAnswered == 2)
        {
            MaleSpeechBubble.GetComponentInChildren<TMPro.TMP_Text>().text = feedbackAnswer2[index];
            MaleSpeechBubble.SetActive(true);
            continueButton.SetActive(true);
            MaleSpeaker.SetTrigger("feedback2");
            FemaleSpeaker.SetTrigger("feedback2");
            WrongMusicAudio.Play();
            MaleFeedbackAudio.Play();
            FemaleWrongAnswerAudio.Play();
            FemaleTalkingAudio.Stop();
        }
        else //continue button is pressed after male finishes a sentence
        {
            WrongMusicAudio.Stop();
            MaleFeedbackAudio.Stop();
            FemaleWrongAnswerAudio.Stop();
            CorrectMusicAudio.Stop();
            MaleFeedbackAudio.Stop();
            FemaleCorrectAnswerAudio.Stop();

            //MaleSpeechBubble.SetActive(false);
            answer1Button.SetActive(false);
            answer2Button.SetActive(false);
            FemaleSpeechBubble.SetActive(false);
            
            MaleSpeaker.SetTrigger("startAsking");
            FemaleSpeaker.SetTrigger("startListening");
            if(index < sentences.Length - 1)
            {
                index++;
                
                StartCoroutine(TypeMale());
            }
            else
            {
                continueButton.SetActive(false);
                FemaleSpeechBubble.SetActive(false);
                MaleSpeechBubble.SetActive(false);
                FemaleSpeaker.SetTrigger("startListening");
                MaleSpeaker.SetTrigger("startListening");
            }
        }
        hasAnswered = 0;       
        FemaleSpeechBubble.SetActive(false);
    }
}
