using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfter : MonoBehaviour
{
    public int seconds;

    void OnEnable()
    {
        StartCoroutine(DisableMe(seconds));
    }

    public IEnumerator DisableMe(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.gameObject.SetActive(false);
    }
}
