using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] sfx;

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
