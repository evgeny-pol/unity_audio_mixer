using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundVolumeToggle : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [AudioParamName][SerializeField] private string _volumeParameterName;
    [PlayerPrefName][SerializeField] private string _playerPrefName;

    private void Start()
    {
        _toggle.onValueChanged.AddListener(OnToggled);
        _toggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt(_playerPrefName, Convert.ToInt32(true)));
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnToggled);
    }

    private void OnToggled(bool isEnabled)
    {
        _audioMixerGroup.audioMixer.SetFloat(_volumeParameterName, isEnabled ? SoundConstants.VolumeMaxDb : SoundConstants.VolumeMinDb);
        PlayerPrefs.SetInt(_playerPrefName, Convert.ToInt32(isEnabled));
    }
}
