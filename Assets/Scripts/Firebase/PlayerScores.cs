using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using FullSerializer;
using System.Linq;
using System.Text.RegularExpressions;



public class PlayerScores : MonoBehaviour
{
    public static PlayerScores Instance;
    public InputField usernameText, userPasswordText;

    public static int userCorrects, userFalses;
    public static string userName, date, userPassword, userSchool;

    public static string databaseURL = "https://math-game-671a6-default-rtdb.firebaseio.com/";

    User user = new User(userName, userPassword, userSchool, userCorrects, userFalses, date);

    private static fsSerializer serializer = new fsSerializer();
    public delegate void PostUserCallback();

    public delegate void GetUserCallback(User user);
    public delegate void GetUsersCallback(Dictionary<string, User> users);

    public int userAmount = 0;
    public int listAmout = 0;
    [SerializeField] private Transform leaderboardContent;
    [SerializeField] private GameObject userPrefab;
    [SerializeField] private GameObject errorText;
    [SerializeField] private GameObject userInfo;
    [SerializeField] private TMP_InputField schoolNameInp;

    private bool userExist;

    private static readonly Regex regex = new Regex("^[a-zA-Z0-9]*$");
    private static readonly Regex regex1 = new Regex("^[a-z]*$");
    private static readonly Regex regex2 = new Regex("^[A-Z]*$");
    private static readonly Regex regex3 = new Regex("^[0-9]*$");

    List<UserValues> userValues = new List<UserValues>();

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
        userName = usernameText.text;
        userPassword = userPasswordText.text;
        userSchool = schoolNameInp.text;
        userCorrects = Processes.Instance.correctNum;
        userFalses = Processes.Instance.falseNum;
        PostToDatabase();
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
        User user = new User(userName, userPassword, userSchool,userCorrects, userFalses, date);
        RestClient.Put($"{databaseURL}users/{userName}.json", user).Then(response => {
            Debug.Log("The user was successfully uploaded to the database");
        });
    }
    
    private void RetrieveFromDatabase()
    {
        RestClient.Get<User>(databaseURL + "users/" + PlayerPrefs.GetString("username") + ".json").Then(response => 
        {
            user = response;
            //UpdateScore();
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

    public void LoginFunc()
    {
        StartCoroutine(LoginButton());
    }

    IEnumerator LoginButton()
    {
        GetUserExist();
        Debug.Log("First");
        yield return new WaitForSeconds(.75f);
        GetUserandPassword();
        Debug.Log("Second");
    }

    public void GetUserExist()
    {
        GetUsers(users =>
        {
            foreach (var user in users)
            {
                if (user.Value.userName == usernameText.text)
                {
                    userExist = true;
                    Debug.Log("User exists");
                    break;
                }
                else
                {
                    userExist = false;
                    Debug.Log("User does not exist");
                }
            }
        });
    }

    public void GetUserandPassword()
    {
        GetUser(usernameText.text, users =>
        {
            if (userExist)
            {
                if (userPasswordText.text == users.userPassword)
                {
                    errorText.SetActive(true);
                    errorText.GetComponent<Text>().color = Color.green;
                    errorText.GetComponent<Text>().text = "Succesfully logged in";
                    Debug.Log("B1");
                    StartCoroutine(PanelGoMainBackground());
                    schoolNameInp.text = users.userSchool;
                }
                else
                {
                    Debug.Log("B2");
                    errorText.SetActive(true);
                    errorText.GetComponent<Text>().color = Color.red;
                    errorText.GetComponent<Text>().text = "Wrong password!";
                }
            }
            else
            {
                if (userPasswordText.text.Any(char.IsDigit) && userPasswordText.text.Any(char.IsLetter) && userPasswordText.text.Length >= 6 && userPasswordText.text != "")
                {
                    Debug.Log("B3");
                    errorText.SetActive(true);
                    errorText.GetComponent<Text>().color = Color.green;
                    errorText.GetComponent<Text>().text = "Succesfully logged in";

                    userExist = true;
                    StartCoroutine(PanelGoMainBackground());
                }
                else
                {
                    Debug.Log("B4");
                    errorText.SetActive(true);
                    errorText.GetComponent<Text>().color = Color.red;
                    errorText.GetComponent<Text>().text = "Password must contain at least six characters, including one alphabetic character and one number";
                }
            }
        });
    }

    IEnumerator PanelGoMainBackground()
    {
        if (userExist)
        {
            yield return new WaitForSeconds(1f);
            ButtonOnClick.Instance.ButtonSound();
            ButtonOnClick.Instance.PanelBackgroundMain();
            //Debug.Log("Girdim");
        }
    }

    public void GetGetUsers()
    {
        GetUsers(users =>
        {
            foreach (var user in users)
            {
                //Debug.Log(user.Value.userName);
                if (user.Value.userName == PlayerPrefs.GetString("username"))
                {
                    //Debug.Log(user.Value.userName);
                    //Debug.Log("userCorrects:" + " " + user.Value.userCorrects);
                    //Debug.Log("userFalses:" + " " + user.Value.userFalses);

                    PlayerPrefs.SetInt("correctNum", user.Value.userCorrects);
                    //Debug.Log(PlayerPrefs.GetInt("correctNum"));
                    PlayerPrefs.SetInt("falseNum", user.Value.userFalses);
                    //Debug.Log(PlayerPrefs.GetInt("falseNum"));

                    Processes.Instance.correctNum = user.Value.userCorrects;
                    Processes.Instance.falseNum = user.Value.userFalses;

                    Processes.Instance.rights.text = "RIGHTS :" + " " + PlayerPrefs.GetInt("correctNum");
                    Processes.Instance.wrongs.text = "WRONGS :" + " " + PlayerPrefs.GetInt("falseNum");
                }
            }
        });
    }

    public void GetLeaderboard()
    {
        GetUsersForLeaderboard(users =>
        {
            foreach (Transform contenct in leaderboardContent)
            {
                Destroy(contenct.gameObject);
            }

            foreach (var user in users)
            {
                //Debug.Log("userAmount is:"  + " " + userAmount);
                userAmount++;
                //var itemGo = Instantiate(userPrefab);
                //itemGo.transform.SetParent(leaderboardContent);
                //itemGo.transform.localScale = Vector2.one;

                //Text[] texts = itemGo.GetComponentsInChildren<Text>();
                //texts[0].text = userAmount.ToString();
                //texts[1].text = user.Value.userName;
                //texts[2].text = user.Value.userCorrects.ToString();
                //texts[3].text = user.Value.userFalses.ToString();

                userValues.Add(new UserValues(user.Value.userName, user.Value.userCorrects, user.Value.userFalses));
                //Debug.Log(userValues[userAmount]);
            }

            userAmount = 0;
            userValues.Sort();
            userValues.Reverse();

            foreach (UserValues values in userValues)
            {
                listAmout++;
                //Debug.Log(values.name + " " + " and " + " " + values.corrects + " " + "and" + " " + values.falses);

                var itemGo = Instantiate(userPrefab);
                itemGo.transform.SetParent(leaderboardContent);
                itemGo.transform.localScale = Vector2.one;

                Text[] texts = itemGo.GetComponentsInChildren<Text>();

                texts[0].text = listAmout.ToString();
                texts[1].text = values.name;
                texts[2].text = values.corrects.ToString();
                texts[3].text = values.falses.ToString();

                if (usernameText.text == values.name)
                {
                    Text[] textss = userInfo.GetComponentsInChildren<Text>();

                    textss[0].text = listAmout.ToString();
                    textss[1].text = values.name;
                    textss[2].text = values.corrects.ToString();
                    textss[3].text = values.falses.ToString();
                }

            }
            userValues.Clear();
            listAmout = 0;

            //foreach (var user in users)
            //{
            //    //Debug.Log("userAmount is:"  + " " + userAmount);
            //    //userAmount++;
            //    var itemGo = Instantiate(userPrefab);
            //    itemGo.transform.SetParent(leaderboardContent);
            //    itemGo.transform.localScale = Vector2.one;

            //    Text[] texts = itemGo.GetComponentsInChildren<Text>();
            //    //texts[0].text = userAmount.ToString();
            //    texts[1].text = user.Value.userName;
            //    texts[2].text = user.Value.userCorrects.ToString();
            //    texts[3].text = user.Value.userFalses.ToString();

            //    userValues.Add(new UserValues(user.Value.userName, user.Value.userCorrects, user.Value.userFalses));
            //    //Debug.Log(userValues[userAmount]);

            //}
        });
    }

    public void UserAmount()
    {
        GetUsers(users =>
        {
            foreach (var user in users)
            {
                userAmount++;
            }

        });
    }

    public static void GetUser(string userID, GetUserCallback callback)
    {

        RestClient.Get($"{databaseURL}users/{userID}.json").Then(response =>
        {
            var responseJson = response.Text;

            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(User), ref deserialized);

            var users = deserialized as User;

            callback(users);
        });
    }

    public static void GetUsers(GetUsersCallback callback)
    {
        RestClient.Get($"{databaseURL}users.json").Then(response =>
        {
            var responseJson = response.Text;

            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, User>), ref deserialized);

            var users = deserialized as Dictionary<string, User>;

            callback(users);
        });
    }

    public static void GetUsersForLeaderboard(GetUsersCallback callback)
    {
        RestClient.Get($"{databaseURL}users.json").Then(response =>
        {
            var responseJson = response.Text;

            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, User>), ref deserialized);

            var users = deserialized as Dictionary<string, User>;

            callback(users);
        });
    }
}
