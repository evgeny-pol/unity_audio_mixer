using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    private const float VolumeScale = 20f;
    private const float VolumeCoefMin = 0.0001f;
    private const float VolumeCoefMax = 1f;
    private const int TrueInt = 1;
    private const int FalseInt = 0;

    [SerializeField] private AudioMixerGroup _masterAudioGroup;
    [SerializeField] private AudioMixerGroup _uiAudioGroup;
    [SerializeField] private AudioMixerGroup _musicAudioGroup;
    [SerializeField] private Toggle _soundsEnabledToggle;
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _uiVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;

    private void Start()
    {
        _soundsEnabledToggle.onValueChanged.AddListener(OnSetSoundsEnabled);
        _masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        _uiVolumeSlider.onValueChanged.AddListener(OnUIVolumeChanged);
        _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        _soundsEnabledToggle.isOn = PlayerPrefs.GetInt(PlayerPrefNames.SoundsEnabled, TrueInt) == TrueInt;
        _masterVolumeSlider.value = PlayerPrefs.GetFloat(PlayerPrefNames.MasterVolume, VolumeCoefMax);
        _uiVolumeSlider.value = PlayerPrefs.GetFloat(PlayerPrefNames.UIVolume, VolumeCoefMax);
        _musicVolumeSlider.value = PlayerPrefs.GetFloat(PlayerPrefNames.MusicVolume, VolumeCoefMax);
    }

    private void OnDisable()
    {
        _soundsEnabledToggle.onValueChanged.RemoveListener(OnSetSoundsEnabled);
        _masterVolumeSlider.onValueChanged.RemoveListener(OnMasterVolumeChanged);
        _uiVolumeSlider.onValueChanged.RemoveListener(OnUIVolumeChanged);
        _musicVolumeSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);
    }

    public void OnSetSoundsEnabled(bool isEnabled)
    {
        SetMasterVolume(isEnabled ? _masterVolumeSlider.value : VolumeCoefMin);
        PlayerPrefs.SetInt(PlayerPrefNames.SoundsEnabled, isEnabled ? TrueInt : FalseInt);
    }

    public void OnMasterVolumeChanged(float volumeCoef)
    {
        if (_soundsEnabledToggle.isOn)
            SetMasterVolume(volumeCoef);

        PlayerPrefs.SetFloat(PlayerPrefNames.MasterVolume, volumeCoef);
    }

    private void OnUIVolumeChanged(float volumeCoef)
    {
        SetMixerVolume(_uiAudioGroup, AudioParams.UIVolume, volumeCoef);
        PlayerPrefs.SetFloat(PlayerPrefNames.UIVolume, volumeCoef);
    }

    private void OnMusicVolumeChanged(float volumeCoef)
    {
        SetMixerVolume(_musicAudioGroup, AudioParams.MusicVolume, volumeCoef);
        PlayerPrefs.SetFloat(PlayerPrefNames.MusicVolume, volumeCoef);
    }

    private void SetMasterVolume(float volumeCoef)
    {
        SetMixerVolume(_masterAudioGroup, AudioParams.MasterVolume, volumeCoef);
    }

    private void SetMixerVolume(AudioMixerGroup audioMixerGroup, string paramName, float volumeCoef)
    {
        audioMixerGroup.audioMixer.SetFloat(paramName, Mathf.Log10(volumeCoef) * VolumeScale);
    }
}
