using UnityEngine;

public class WinSceneManager : MonoBehaviour
{
    void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayWin();
        }
    }
}
