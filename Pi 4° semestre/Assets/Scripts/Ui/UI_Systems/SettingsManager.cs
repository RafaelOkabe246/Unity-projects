using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer masterAudioMixer;

    [Header("Sound Settings")]
    [SerializeField] private Slider masterSlide;
    private float masterValue;
    public TextMeshProUGUI masterVolumeValue;

    [SerializeField] private Slider musicSlide;
    private float musicValue;
    public TextMeshProUGUI musicVolumeValue;

    [SerializeField] private Slider sfxSlide;
    private float sfxValue;
    public TextMeshProUGUI SfxVolumeValue;

    [Header("Graphics Settings")]
    public TMP_Dropdown resolutionDropdown;
    List<Resolution> resolutions;

    private void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        FillResolutionsDropDown();
        Screen.SetResolution(1920, 1080, true);
    }

    private void OnEnable()
    {
        SetMasterVolume(masterSlide.value);
        SetMusicVolume(musicSlide.value);
        SetSFXVolume(sfxSlide.value);
    }

    private void FillResolutionsDropDown()
    {
        resolutions = new List<Resolution>();

        foreach (Resolution res in Screen.resolutions)
        {
            bool res1 = ((res.width == 1920 && res.height == 1080) && !resolutions.Contains(res));
            bool res2 = ((res.width == 1600 && res.height == 900) && !resolutions.Contains(res));
            bool res3 = ((res.width == 1280 && res.height == 720) && !resolutions.Contains(res));

            if (res1 || res2 || res3)
                resolutions.Add(res);
        }

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Count; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == 1920 && resolutions[i].height == 1080)
                currentResolutionIndex = i;
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        SetResolution(currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();
    }

    public void SetMasterVolume(float value)
    {
        if (value <= 0)
        {
            value = -40;

            masterAudioMixer.SetFloat("Master", Mathf.Log10(value) * 20);
            masterVolumeValue.text = "" + 0;
            masterValue = value;
            return;
        }
        else
        {
            masterAudioMixer.SetFloat("Master", Mathf.Log10(value) * 20);
            masterVolumeValue.text = "" + (Mathf.Floor(value * 100));
            masterValue = value;
        }

    }

    public void SetMusicVolume(float value)
    {
        if (value <= 0)
        {
            value = -40;

            masterAudioMixer.SetFloat("Music", Mathf.Log10(value) * 20);
            musicVolumeValue.text = "" + 0;
            musicValue = value;
        }
        else
        {
            masterAudioMixer.SetFloat("Music", Mathf.Log10(value) * 20);
            musicVolumeValue.text = "" + (Mathf.Floor(value * 100));
            musicValue = value;
        }

    }

    public void SetSFXVolume(float value)
    {
        if (value <= 0)
        {
            value = -40;

            masterAudioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
            SfxVolumeValue.text = "" + 0;
            sfxValue = value;
        }
        else
        {
            masterAudioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
            SfxVolumeValue.text = "" + (Mathf.Floor(value * 100));
            sfxValue = value;
        }

    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
