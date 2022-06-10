using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveUsername : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables
    
    [SerializeField] private InputField username;
    [SerializeField] private Text usernameText;

    [SerializeField] private Text[] texts;


    #endregion

    #endregion
    private void Awake()
    {
        if (PlayerPrefs.HasKey("username"))
        {
            username.text = PlayerPrefs.GetString("username").ToString();
            usernameText.text = PlayerPrefs.GetString("username").ToString();
        }
    }
    public void SaveUsernameInput()
    {
        PlayerPrefs.SetString("username", username.text);
        //Debug.Log(PlayerPrefs.GetString("username"));
    }

    private void FixedUpdate()
    {
        //usernameText.text = PlayerPrefs.GetString("username").ToString();
    }

    public void OnValueChange()
    {
        usernameText.text = username.text;
        
        for (int i = 0; i <7; i++)
        {
            texts[i].text = username.text;
        }

    }
}
