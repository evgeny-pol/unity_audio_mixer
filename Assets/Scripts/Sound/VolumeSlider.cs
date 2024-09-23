using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private const float VolumeScale = 20f;

    [SerializeField] private Slider _slider;
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [AudioParamName][SerializeField] private string _volumeParameterName;
    [PlayerPrefName][SerializeField] private string _playerPrefName;

    private void Start()
    {
        _slider.onValueChanged.AddListener(OnVolumeChanged);
        _slider.value = PlayerPrefs.GetFloat(_playerPrefName, _slider.value);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnVolumeChanged);
    }

    public void OnVolumeChanged(float volumeCoef)
    {
        _audioMixerGroup.audioMixer.SetFloat(_volumeParameterName, Mathf.Log10(volumeCoef) * VolumeScale);
        PlayerPrefs.SetFloat(_playerPrefName, volumeCoef);
    }
}
