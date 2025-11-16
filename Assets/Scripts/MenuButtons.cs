using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
