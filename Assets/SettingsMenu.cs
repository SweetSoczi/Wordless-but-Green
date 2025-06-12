using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider Music_Volume;
    [SerializeField] private Slider Master_Volume;
    [SerializeField] private Slider SFX_Volume;

    public TMPro.TMP_Dropdown ResolutionDropdown;

    Resolution[] resolutions;


    private void Start()
    {
        resolutions = Screen.resolutions;

        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        List<Resolution> filteredResolutions = new List<Resolution>();

        int currentResolutionIndex = 0;

        List<Vector2Int> allowedResolutions = new List<Vector2Int>
    {
        new Vector2Int(1920, 1080),
        new Vector2Int(2560, 1440),
        new Vector2Int(3440, 1440)
    };

        List<int> allowedRefreshRates = new List<int> { 60, 144, 160 };

        foreach (Resolution res in resolutions)
        {
            Vector2Int resVector = new Vector2Int(res.width, res.height);
            int roundedHz = Mathf.RoundToInt((float)res.refreshRateRatio.value);

            if (allowedResolutions.Contains(resVector) && allowedRefreshRates.Contains(roundedHz))
            {
                string option = res.width + " x " + res.height + " @ " + roundedHz + "Hz";

                if (!options.Contains(option))
                {
                    options.Add(option);
                    filteredResolutions.Add(res);
                }
            }
        }

        ResolutionDropdown.AddOptions(options);
        resolutions = filteredResolutions.ToArray();

        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            currentResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");

            if (currentResolutionIndex < 0 || currentResolutionIndex >= resolutions.Length)
            {
                currentResolutionIndex = 0;
            }
        }
        else
        {
            for (int i = 0; i < resolutions.Length; i++)
            {
                int screenHz = Mathf.RoundToInt((float)Screen.currentResolution.refreshRateRatio.value);
                int optionHz = Mathf.RoundToInt((float)resolutions[i].refreshRateRatio.value);

                if (resolutions[i].width == Screen.width &&
                    resolutions[i].height == Screen.height &&
                    optionHz == screenHz)
                {
                    currentResolutionIndex = i;
                    break;
                }
            }
        }

        ResolutionDropdown.value = currentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();

        SetResolution(currentResolutionIndex);

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetMasterVolume();
            SetSFXVolume();
        }
    }


    public void SetMusicVolume()
    {
        float volume = Music_Volume.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetMasterVolume()
    {
        float volume = Master_Volume.value;
        myMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = SFX_Volume.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolume()
    {
        Music_Volume.value = PlayerPrefs.GetFloat("MusicVolume");
        Master_Volume.value = PlayerPrefs.GetFloat("MasterVolume");
        SFX_Volume.value = PlayerPrefs.GetFloat("SFXVolume");

        SetMusicVolume();
        SetMasterVolume();
        SetSFXVolume();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        int refreshRate = Mathf.RoundToInt((float)resolution.refreshRateRatio.value);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, refreshRate);

        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
    }
}
