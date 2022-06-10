using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class User : MonoBehaviour
{
    public static User Instance;

    public string userName;
    public string userPassword;
    public string userSchool;
    public int userCorrects;
    public int userFalses;
    public string date;
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
    public User(string userName, string userPassword, string userSchool, int userCorrects, int userFalses, string date)
    {
        this.userName = userName;
        this.userPassword = userPassword;
        this.userSchool = userSchool;
        this.userCorrects = userCorrects;
        this.userFalses = userFalses;
        this.date = date;
    }
}
