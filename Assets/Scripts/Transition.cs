using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadStartMenu()
    {
        StartCoroutine(LoadLevel("MainMenu"));
    }
    public void LoadCredits()
    {
        StartCoroutine(LoadLevel("Credits"));
    }
    public void LoadGame()
    {
        StartCoroutine(LoadLevel("Difficulty 1"));
    }


    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
