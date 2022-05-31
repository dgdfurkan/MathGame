using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Processes : MonoBehaviour
{
    public static Processes Instance;

    #region Self Variables

    #region Serializable Variables

    [SerializeField] private Text rights, wrongs, result, number1Text, processorText, number2Text; 
    [SerializeField] private InputField resultNumber;
    private int number1, processor, number2;
    private int resultInScript, correctNum, falseNum;
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
        if (PlayerPrefs.HasKey("correctNum"))
        {
            correctNum = PlayerPrefs.GetInt("correctNum");
            rights.text = "RIGHTS :" + " " + correctNum;
        }
        if (PlayerPrefs.HasKey("falseNum"))
        {
            falseNum = PlayerPrefs.GetInt("falseNum");
            wrongs.text = "WRONGS :" + " " + falseNum;
        }
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

        if (resultNumber.text != "")
        {
            if (int.Parse(resultNumber.text) == resultInScript)
            {
                result.GetComponent<Animator>().SetBool("Correct", true);
                correctNum++;
                PlayerPrefs.SetInt("correctNum", correctNum);
                rights.text = "RIGHTS :" + " " + correctNum;
                result.text = "CORRECT";
            }
            else
            {
                result.GetComponent<Text>().color = Color.red;
                falseNum++;
                PlayerPrefs.SetInt("falseNum", falseNum);
                wrongs.text = "WRONGS :" + " " + falseNum;
                result.text = "FALSE";
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
    }

    IEnumerator Delay()
    {
        resultNumber.GetComponent<InputField>().enabled = false;
        yield return new WaitForSeconds(0.6f);
        NewQuestion();
    }
    public void NewQuestion()
    {
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
