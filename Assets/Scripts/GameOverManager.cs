using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayLose();
        }
    }
}
