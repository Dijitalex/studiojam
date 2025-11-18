using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer Instance { get; private set; }
    public AudioSource audioSource;
    public AudioClip[] sfx;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    public void PlaySFX(string clipName)
    {
        foreach (AudioClip clip in sfx)
        {
            if (clip != null && clip.name == clipName)
            {
                audioSource.PlayOneShot(clip);
                break;
            }
        }
    }
}
