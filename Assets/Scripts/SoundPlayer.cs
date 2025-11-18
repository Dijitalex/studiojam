using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    public AudioClip background;

    public AudioClip submit;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip buttonClick;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (musicSource != null && background != null)
        {
            musicSource.clip = background;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlaySubmit()
    {
        if (sfxSource != null && submit != null)
        {
            sfxSource.PlayOneShot(submit);
        }
    }

    public void PlayWin()
    {
        if (sfxSource != null && win != null)
        {
            sfxSource.PlayOneShot(win);
        }
    }

    public void PlayLose()
    {
        if (sfxSource != null && lose != null)
        {
            sfxSource.PlayOneShot(lose);
        }
    }

    public void PlayButtonClick()
    {
        if (sfxSource != null && buttonClick != null)
        {
            sfxSource.PlayOneShot(buttonClick);
        }
    }
}