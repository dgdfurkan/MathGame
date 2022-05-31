using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyValue : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private Slider difficultySlider;
    [SerializeField] private Text difficultyText;

    #endregion

    #endregion

    private void Start()
    {
        if (PlayerPrefs.HasKey("difficultyValue"))
        {
            difficultySlider.value = PlayerPrefs.GetFloat("difficultyValue");
            difficultyText.text = PlayerPrefs.GetFloat("difficultyValue").ToString("0.00");
        }
        difficultySlider.onValueChanged.AddListener((v) =>
        {
            difficultyText.text = v.ToString("0.00");
            PlayerPrefs.SetFloat("difficultyValue", v);
        });
    }
}