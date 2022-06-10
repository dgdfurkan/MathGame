using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private ScrollRect leaderboardScrool;
    [SerializeField] private Transform leaderboardContent;
    [SerializeField] private GameObject userPrefab;

    private void Start()
    {
        //PlayerScores.Instance.UserAmount();
    }

    public void CreateLeaderboard()
    {
        //Debug.Log("oq");
        //for (int i = 0; i <= PlayerScores.Instance.userAmount - 1; i++)
        //{
        //    Debug.Log("oliyiii");
        //    var itemGo = Instantiate(userPrefab);
        //    itemGo.transform.SetParent(leaderboardContent);
        //    itemGo.transform.localScale = Vector2.one;
        //}
    }
}
