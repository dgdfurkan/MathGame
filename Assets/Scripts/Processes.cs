using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Processes : MonoBehaviour
{
    public static Processes Instance;

    #region Self Variables

    #region Serializable Variables

    public Text rights, wrongs, result, number1Text, processorText, number2Text; 
    [SerializeField] private InputField resultNumber;
    private int number1, processor, number2;
    public int resultInScript, correctNum, falseNum, correctStreak = 0, falseStreak = 0;
    [SerializeField] private AudioSource correctSound1, correctSound2, correctSound3, falseSound1;
    [SerializeField] private GameObject helpButton, helpPanel;
    private int randomNumber, lastNumber;
    private float timerForHelp;
    private bool timerForBool;

    #endregion

    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        NewQuestion();
        //if (PlayerPrefs.HasKey("correctNum"))
        //{
        //    correctNum = PlayerPrefs.GetInt("correctNum");
        //    rights.text = "RIGHTS :" + " " + correctNum;
        //}
        //if (PlayerPrefs.HasKey("falseNum"))
        //{
        //    falseNum = PlayerPrefs.GetInt("falseNum");
        //    wrongs.text = "WRONGS :" + " " + falseNum;
        //}
        timerForBool = true;
    }

    private void Update()
    {
        if (timerForBool)
        {
            timerForHelp += Time.deltaTime;
            //Debug.Log(timerForHelp);
        }
        
        if (timerForHelp >= 6)
        {
            OpenHelp();
        }
    }

    private void OpenHelp()
    {
        helpButton.SetActive(true);
        timerForBool = false;
        timerForHelp = 0f;
    }

    public void HelpFromTeacher()
    {
        helpButton.GetComponent<Button>().interactable = false;
        helpPanel.SetActive(true);
        Text[] options = helpPanel.GetComponentsInChildren<Text>();

        for (int i = 0; i < options.Length; i++)
        {
            if (resultInScript != 0)
            {
                options[i].text = Random.Range(resultInScript/3 ,resultInScript*3).ToString();
                //Debug.Log(options[i].text);
            }
        }

        int e = Random.Range(0, options.Length - 1);

        for (int i = 0; i < options.Length; i++)
        {
            if (options[i].text == resultInScript.ToString())
            {
                //options[e].text = resultInScript.ToString();
                goto sssss;
            }
            if (options[i].text != resultInScript.ToString())
            {
                options[e].text = resultInScript.ToString();
            }
            sssss:
            break;
        }
    }

    public void ClickOnOptions()
    {
        PlayerPrefs.SetString("optionSelected", EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<Text>().text);
        resultNumber.text = PlayerPrefs.GetString("optionSelected");

        ResultControl();
        //Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    }

    public void ResultControl()
    {
        //if (resultNumber.text != null)
        //{
        //    PlayerPrefs.SetFloat("resultNumber", int.Parse(resultNumber.text));
        //}
        //else
        //{
        //    PlayerPrefs.SetFloat("resultNumber", 0f);
        //}

        //int resultNumberInt = int.Parse(resultNumber.text);
        timerForBool = false;
        timerForHelp = 0f;

        if (resultNumber.text != "")
        {
            if (int.Parse(resultNumber.text) == resultInScript)
            {
                result.GetComponent<Animator>().SetBool("Correct", true);
                correctNum++;
                PlayerPrefs.SetInt("correctNum", correctNum);
                rights.text = "RIGHTS :" + " " + correctNum;
                result.text = "CORRECT";

                correctStreak += 1;
                if (falseStreak != 0)
                {
                    falseStreak -= 1;
                }
                switch (correctStreak)
                {
                    case 1:
                        correctSound1.Play();
                        break;
                    case 2:
                        correctSound2.Play();
                        break;
                    default:
                        correctSound3.Play();
                        break;
                }
            }
            else
            {
                result.GetComponent<Text>().color = Color.red;
                falseNum++;
                PlayerPrefs.SetInt("falseNum", falseNum);
                wrongs.text = "WRONGS :" + " " + falseNum;
                result.text = "FALSE";

                if (falseStreak == 0)
                {
                    falseStreak += 1;
                }
                if (correctStreak != 0)
                {
                    correctStreak = 0;
                }
                falseSound1.Play();
            }
            StartCoroutine(Delay());
        }
        //if (int.Parse(resultNumber.text) == resultInScript)
        //{
        //    correctNum++;
        //    rights.text = "RIGHTS :" + " " + correctNum;
        //    result.text = "CORRECT";
        //}
        //else
        //{
        //    falseNum++;
        //    wrongs.text = "WRONGS :" + " " + falseNum;
        //    result.text = "FALSE";
        //}
        PlayerScores.Instance.OnSubmit();
    }

    IEnumerator Delay()
    {
        resultNumber.GetComponent<InputField>().enabled = false;
        yield return new WaitForSeconds(0.6f);
        NewQuestion();
    }
    public void NewQuestion()
    {
        timerForBool = true;
        timerForHelp = 0f;
        helpButton.SetActive(false);
        helpPanel.SetActive(false);
        helpButton.GetComponent<Button>().interactable = true;
        resultNumber.GetComponent<InputField>().enabled = true;
        resultNumber.ActivateInputField();
        resultNumber.Select();
        resultNumber.text = "";
        result.text = "";
        //result.GetComponent<Text>().color = Color.white;
        result.GetComponent<Animator>().SetBool("Correct", false);

        if (PlayerPrefs.GetFloat("difficultyValue") < 33.33f)
        {
            number1 = Random.Range(0, 20);
            number2 = Random.Range(0, 20);
        }else if (PlayerPrefs.GetFloat("difficultyValue") >= 33.33f && PlayerPrefs.GetFloat("difficultyValue") < 66.66f)
        {
            number1 = Random.Range(0, 50);
            number2 = Random.Range(0, 50);
        }
        else
        {
            number1 = Random.Range(0, 100);
            number2 = Random.Range(0, 100);
        }
        processor = Random.Range(1, 4);

        switch(PlayerPrefs.GetFloat("processValue"))
        {
            case 0:
                switch (processor)
                {
                    case 1:
                        processorText.text = "+";
                        resultInScript = number1 + number2;
                        break;
                    case 2:
                        processorText.text = "-";
                        resultInScript = number1 - number2;
                        break;
                    case 3:
                        processorText.text = "*";
                        resultInScript = number1 * number2;
                        break;
                    case 4:
                        processorText.text = "/";
                        if (number2 == 0)
                        {
                            number2 = 1;
                        }
                        resultInScript = number1 / number2;
                        break;
                }
                break;
            case 1:
                processorText.text = "+";
                resultInScript = number1 + number2;
                break;
            case 2:
                processorText.text = "-";
                resultInScript = number1 - number2;
                break;
            case 3:
                processorText.text = "*";
                resultInScript = number1 * number2;
                break;
            case 4:
                processorText.text = "/";
                if (number2 == 0)
                {
                    number2 = 1;
                }
                resultInScript = number1 / number2;
                break;
        }
        number1Text.text = number1 + "";
        number2Text.text = number2 + "";

        
    }
}
