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
    private float SfxValue;
    public TextMeshProUGUI SfxVolumeValue;

    [Header("Graphics Settings")]
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    private void Start()
    {
        FillResolutionsDropDown();

    }

    private void OnEnable()
    {
        SetMasterVolume(masterSlide.value);
        SetMusicVolume(musicSlide.value);
        SetSFXVolume(sfxSlide.value);
    }

    private void FillResolutionsDropDown() 
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;

            if ((resolutions[i].width == 1920 && resolutions[i].height == 1080) || 
                (resolutions[i].width == 1600 && resolutions[i].height == 900) || 
                (resolutions[i].width == 1280 && resolutions[i].height == 720))
                options.Add(option);

            if (resolutions[i].width == 1920 && resolutions[i].height == 1080)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetMasterVolume(float value)
    {
        masterAudioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
        masterVolumeValue.text = "" + (Mathf.Floor(value * 100));
        masterValue = value;
    }

    public void SetMusicVolume(float value)
    {
        masterAudioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
        musicVolumeValue.text = "" + (Mathf.Floor(value * 100));
        musicValue = value;
    }

    public void SetSFXVolume(float value)
    {
        masterAudioMixer.SetFloat("SfxVolume", Mathf.Log10(value) * 20);
        SfxVolumeValue.text = "" + (Mathf.Floor(value * 100));
        SfxValue = value;
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
