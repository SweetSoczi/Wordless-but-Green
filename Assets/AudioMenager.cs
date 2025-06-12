using UnityEngine;

public class AudioMenager : MonoBehaviour
{
    [Header("---Audio Source---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---Audio Clip---")]
    public AudioClip MenuTheme;
    public AudioClip Death;
    public AudioClip Hurt;
    public AudioClip Jump;
    public AudioClip Hit;
    public AudioClip Run;
    public AudioClip PressButton;
    public AudioClip SwitchButtons;

    private void Start()
    {
        musicSource.clip = MenuTheme;
        musicSource.Play();
    }
}
