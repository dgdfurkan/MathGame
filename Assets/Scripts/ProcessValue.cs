using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessValue : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private Slider processSlider;
    [SerializeField] private Text processText;

    #endregion

    #endregion

    private void Start()
    {
        if (PlayerPrefs.HasKey("processValue"))
        {
            processSlider.value = PlayerPrefs.GetFloat("processValue");
            switch (PlayerPrefs.GetFloat("processValue"))
            {
                case 0:
                    processText.text = "Process is: Hybrid";
                    break;
                case 1:
                    processText.text = "Process is: Addition";
                    break;
                case 2:
                    processText.text = "Process is: Subtraction";
                    break;
                case 3:
                    processText.text = "Process is: Multiplication";
                    break;
                case 4:
                    processText.text = "Process is: Division";
                    break;
            }
            //processText.text = PlayerPrefs.GetFloat("processValue").ToString();
        }
        processSlider.onValueChanged.AddListener((v) =>
        {
            PlayerPrefs.SetFloat("processValue", v);
            switch (PlayerPrefs.GetFloat("processValue"))
            {
                case 0:
                    processText.text = "Process is: Hybrid";
                    break;
                case 1:
                    processText.text = "Process is: Addition";
                    break;
                case 2:
                    processText.text = "Process is: Subtraction";
                    break;
                case 3:
                    processText.text = "Process is: Multiplication";
                    break;
                case 4:
                    processText.text = "Process is: Division";
                    break;
            }
            PlayerPrefs.SetFloat("processValue", v);
            //processText.text = v.ToString();
        });
    }
}
