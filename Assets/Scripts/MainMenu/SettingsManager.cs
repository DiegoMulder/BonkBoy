using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

public class SettingsManager : MonoBehaviour
{
    [Header("Volume Setting")]
    public Slider masterVolume_Slider, SFXVolume_Slider, AmbienceVolume_Slider;
    private float masterVolume_Value, SFXVolume_Value, AmbienceVolume_Value;
    public TextMeshProUGUI masterVolume_Value_Text, SFXVolume_Value_Text, AmbienceVolume_Value_Text;
    public AudioMixer audioMixer;

    [Header("Resolution Setting")]
    public TMP_Dropdown resolutionDropdown;
    private List<Resolution> customResolutions = new List<Resolution>()
    {
        new Resolution { width = 640, height = 480 },
        new Resolution { width = 800, height = 600 },
        new Resolution { width = 1024, height = 768 },
        new Resolution { width = 1280, height = 720 },
        new Resolution { width = 1920, height = 1080 },
        new Resolution { width = 2560, height = 1440 },
        new Resolution { width = 3840, height = 2160 }
    };

    [Header("Anti-Aliasing Setting")]
    public TMP_Dropdown antiAliasingDropdown;
    public UniversalRenderPipelineAsset urpAsset;
    public Camera[] cameraObjects;

    [Header("Texture Quality Setting")]
    public TMP_Dropdown textureQualityDropdown;

    [Header("FOV Setting")]
    public Slider fovSlider;
    public TextMeshProUGUI fovValueText;
    private float fovValue = 60;

    [Header("Brightness Setting")]
    public Slider brightnessSlider;
    public TextMeshProUGUI brightnessValueText;
    private float brightnessValue = 20;

    private void Start()
    {
        LoadSettings();

        // Volume Listeners
        masterVolume_Slider.onValueChanged.AddListener(delegate { SetMasterVolume(masterVolume_Slider.value); });
        SFXVolume_Slider.onValueChanged.AddListener(delegate { SetSFXVolume(SFXVolume_Slider.value); });
        AmbienceVolume_Slider.onValueChanged.AddListener(delegate { SetAmbienceVolume(AmbienceVolume_Slider.value); });

        // Resolution Listener
        resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(resolutionDropdown.value); });

        // Anti-Aliasing Dropdown Listener
        antiAliasingDropdown.onValueChanged.AddListener(delegate { SetAntiAliasing(antiAliasingDropdown.value); });

        // Texture Quality Dropdown Listener
        textureQualityDropdown.onValueChanged.AddListener(delegate { SetTextureQuality(textureQualityDropdown.value); });

        // FOV Listener
        fovSlider.onValueChanged.AddListener(delegate { SetFOV(fovSlider.value); });

        // Brightness Listener
        brightnessSlider.onValueChanged.AddListener(delegate { SetBrightness(brightnessSlider.value); });

        SetupResolutionOptions();
        SetupAntiAliasingOptions();
    }

    private void SetupResolutionOptions()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < customResolutions.Count; i++)
        {
            string option = customResolutions[i].width + " x " + customResolutions[i].height;
            options.Add(option);

            if (customResolutions[i].width == Screen.currentResolution.width && customResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        SetResolution(currentResolutionIndex);
    }

    private void SetupAntiAliasingOptions()
    {
        antiAliasingDropdown.ClearOptions();
        List<string> options = new List<string> { "None", "FXAA", "SMAA" };
        antiAliasingDropdown.AddOptions(options);

        int antiAliasingLevel = PlayerPrefs.GetInt("AntiAliasing", 0); // Default: None
        antiAliasingDropdown.value = antiAliasingLevel;
        antiAliasingDropdown.RefreshShownValue();
        SetAntiAliasing(antiAliasingLevel);
    }

    public void SetMasterVolume(float value)
    {
        masterVolume_Value = value;
        audioMixer.SetFloat("MasterVolume", value == 0 ? -80 : Mathf.Log10(value / 100) * 20);
        masterVolume_Value_Text.text = value.ToString("F0");
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        SFXVolume_Value = value;
        audioMixer.SetFloat("SFXVolume", value == 0 ? -80 : Mathf.Log10(value / 100) * 20);
        SFXVolume_Value_Text.text = value.ToString("F0");
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void SetAmbienceVolume(float value)
    {
        AmbienceVolume_Value = value;
        audioMixer.SetFloat("AmbienceVolume", value == 0 ? -80 : Mathf.Log10(value / 100) * 20);
        AmbienceVolume_Value_Text.text = value.ToString("F0");
        PlayerPrefs.SetFloat("AmbienceVolume", value);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = customResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }

    public void SetVSync(bool isVSync)
    {
        QualitySettings.vSyncCount = isVSync ? 1 : 0;
        PlayerPrefs.SetInt("VSync", isVSync ? 1 : 0);
    }

    public void SetAntiAliasing(int level)
    {
        // Stel de anti-aliasing in op alle camera's in de array
        foreach (Camera cam in cameraObjects)
        {
            var cameraData = cam.GetUniversalAdditionalCameraData();
            if (cameraData != null)
            {
                switch (level)
                {
                    case 0: // None
                        cameraData.antialiasing = AntialiasingMode.None;
                        break;
                    case 1: // FXAA
                        cameraData.antialiasing = AntialiasingMode.FastApproximateAntialiasing;
                        break;
                    case 2: // SMAA
                        cameraData.antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing;
                        break;
                    default:
                        cameraData.antialiasing = AntialiasingMode.None;
                        break;
                }
            }
        }

        PlayerPrefs.SetInt("AntiAliasing", level);
    }

    public void SetTextureQuality(int level)
    {
        QualitySettings.masterTextureLimit = level;
        PlayerPrefs.SetInt("TextureQuality", level);
    }

    public void SetFOV(float value)
    {
        fovValue = value;
        foreach (Camera cam in cameraObjects)
        {
            cam.fieldOfView = value;
        }
        fovValueText.text = value.ToString("F0");
        PlayerPrefs.SetFloat("FOV", value);
    }

    public void SetBrightness(float value)
    {
        brightnessValue = value;
        brightnessValueText.text = value.ToString("F0");
        PlayerPrefs.SetFloat("Brightness", value);
        // Brightness adjustment requires post-processing or similar solution.
        // Implement as needed based on your project's needs.
    }

    private void LoadSettings()
    {
        // Volume Settings
        masterVolume_Value = PlayerPrefs.GetFloat("MasterVolume", 100);
        SetMasterVolume(masterVolume_Value);
        masterVolume_Slider.value = masterVolume_Value;

        SFXVolume_Value = PlayerPrefs.GetFloat("SFXVolume", 100);
        SetSFXVolume(SFXVolume_Value);
        SFXVolume_Slider.value = SFXVolume_Value;

        AmbienceVolume_Value = PlayerPrefs.GetFloat("AmbienceVolume", 100);
        SetAmbienceVolume(AmbienceVolume_Value);
        AmbienceVolume_Slider.value = AmbienceVolume_Value;

        // Resolution Settings
        int resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);
        SetResolution(resolutionIndex);
        resolutionDropdown.value = resolutionIndex;

        // Anti-Aliasing Settings
        int antiAliasingLevel = PlayerPrefs.GetInt("AntiAliasing", 0); // Default: None
        SetAntiAliasing(antiAliasingLevel);
        antiAliasingDropdown.value = antiAliasingLevel;

        // Texture Quality Settings
        int textureQuality = PlayerPrefs.GetInt("TextureQuality", 0);
        SetTextureQuality(textureQuality);
        textureQualityDropdown.value = textureQuality;

        // FOV Settings
        fovValue = PlayerPrefs.GetFloat("FOV", 60);
        SetFOV(fovValue);
        fovSlider.value = fovValue;

        // Brightness Settings
        brightnessValue = PlayerPrefs.GetFloat("Brightness", 20);
        SetBrightness(brightnessValue);
        brightnessSlider.value = brightnessValue;
    }
}
