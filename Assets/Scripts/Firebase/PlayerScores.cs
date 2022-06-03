using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerScores : MonoBehaviour
{
    public static PlayerScores Instance;
    public InputField nameText;

    public static int userCorrects, userFalses;
    public static string userName, date;

    public static string databaseURL = "https://math-game-671a6-default-rtdb.firebaseio.com/";

    User user = new User(userName, userCorrects, userFalses, date);

    public delegate void PostUserCallback();

    public delegate void GetUserCallback(User user);
    public delegate void GetUsersCallback(Dictionary<string, User> users);

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

    public void OnSubmit()
    {
        DateTime localDate = DateTime.Now;
        date = localDate.ToString();
        userName = nameText.text;
        userCorrects = Processes.Instance.correctNum;
        userFalses = Processes.Instance.falseNum;
        PostToDatabase();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnGetValues();
        }
    }

    public void OnGetValues()
    {
        RetrieveFromDatabase();
    }

    private void UpdateScore()
    {

        Debug.Log(user.userName);
        Debug.Log("userCorrects:" + " " + user.userCorrects);
        Debug.Log("userFalses:" + " " + user.userFalses);

        Processes.Instance.rights.text = "RIGHTS :" + " " + user.userCorrects;
        Processes.Instance.wrongs.text = "WRONGS :" + " " + user.userFalses;

        PlayerPrefs.SetInt("correctNum", Processes.Instance.correctNum);
        Debug.Log(PlayerPrefs.GetInt("correctNum"));
        PlayerPrefs.SetInt("falseNum", Processes.Instance.falseNum);
        Debug.Log(PlayerPrefs.GetInt("falseNum"));
    }

    private void PostToDatabase()
    {
        User user = new User(userName, userCorrects, userFalses, date);
        RestClient.Put($"{databaseURL}users/{userName}.json", user).Then(response => {
            Debug.Log("The user was successfully uploaded to the database");
        });
    }
    
    private void RetrieveFromDatabase()
    {
        RestClient.Get<User>("https://math-game-671a6-default-rtdb.firebaseio.com/" + "users/" + PlayerPrefs.GetString("username") + ".json").Then(response => 
        {
            user = response;
            UpdateScore();
        });
    }

    public void GetGet()
    {
        GetUser(userName, users =>
        {

            Debug.Log(PlayerPrefs.GetString("username"));
            Debug.Log(user.userName);
            Debug.Log("userCorrects:" + " " + user.userCorrects);
            Debug.Log("userFalses:" + " " + user.userFalses);

            Processes.Instance.rights.text = "RIGHTS :" + " " + user.userCorrects;
            Processes.Instance.wrongs.text = "WRONGS :" + " " + user.userFalses;

            PlayerPrefs.SetInt("correctNum", Processes.Instance.correctNum);
            Debug.Log(PlayerPrefs.GetInt("correctNum"));
            PlayerPrefs.SetInt("falseNum", Processes.Instance.falseNum);
            Debug.Log(PlayerPrefs.GetInt("falseNum"));

        });
    }

    public void GetGetUsers()
    {
        GetUsers(users =>
        {
            if (users == null)
            {
                Debug.Log("No users");
            }

            foreach (var user in users)
            {
                Debug.Log(user.Value.userName);
                if (user.Value.userName == PlayerPrefs.GetString("username"))
                {
                    Debug.Log(user.Value.userName);
                    Debug.Log("userCorrects:" + " " + user.Value.userCorrects);
                    Debug.Log("userFalses:" + " " + user.Value.userFalses);

                    Processes.Instance.rights.text = "RIGHTS :" + " " + user.Value.userCorrects;
                    Processes.Instance.wrongs.text = "WRONGS :" + " " + user.Value.userFalses;

                    PlayerPrefs.SetInt("correctNum", Processes.Instance.correctNum);
                    Debug.Log(PlayerPrefs.GetInt("correctNum"));
                    PlayerPrefs.SetInt("falseNum", Processes.Instance.falseNum);
                    Debug.Log(PlayerPrefs.GetInt("falseNum"));
                }
            }

        });
    }
    
    public static void GetUser(string userName, GetUserCallback callback)
    {

        RestClient.Get($"{databaseURL}users/{userName}.json").Then(response =>
        {
            var responseJson = response.Text;

            object deserialized = null;

            var users = deserialized as User;

            callback(users);
        });
    }

    public static void GetUsers(GetUsersCallback callback)
    {
        RestClient.Get($"{databaseURL}users.json").Then(response =>
        {
            var responseJson = response.Text;

            object deserialized = null;

            var users = deserialized as Dictionary<string, User>;
            
            callback(users);
        });
    }

}
