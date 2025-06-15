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

    public static AudioMenager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
        

    private void Start()
    {
        musicSource.clip = MenuTheme;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
