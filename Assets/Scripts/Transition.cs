using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadStartMenu()
    {
        StartCoroutine(LoadLevel("StartMenu"));
    }
    public void LoadCredits()
    {
        StartCoroutine(LoadLevel("Credits"));
    }
    public void LoadGame()
    {
        StartCoroutine(LoadLevel("MainGame1"));
    }


    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}
