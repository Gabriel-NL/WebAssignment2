using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musSource;
    [SerializeField] AudioSource sfxSource;

    public AudioClip bgMusic;
    public AudioClip engineAccel;
    public AudioClip engineDeccel;

    private void Start()
    {
        musSource.clip = bgMusic;
        musSource.Play();
    }

    public void sfxPlay(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
