using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingGame : MonoBehaviour
{
    [SerializeField] AnimationClip anim;
    private void Awake()
    {
        //gameObject.GetComponent<Animator>().SetBool("IsPlaying", true);
        //Debug.Log(anim.length);
        StartCoroutine(AnimationLenght(anim.length));
    }
    IEnumerator AnimationLenght(float f)
    {
        yield return new WaitForSeconds(f);
        SceneManager.LoadScene("Main Menu");
    }
}
