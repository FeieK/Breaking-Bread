using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mainAudioMixer;
    [SerializeField] private float masterVolumePercent;
    [SerializeField] private TextMeshProUGUI masterVolumeTMP;
    [SerializeField] private float sfxVolumePercent;
    [SerializeField] private TextMeshProUGUI sfxVolumeTMP;

    public void ChangeVolumeMaster(float volume)
    {
        mainAudioMixer.SetFloat("MasterVolume", volume);
        masterVolumePercent = (int)volume + 80;
        masterVolumeTMP.text = masterVolumePercent + "%";
    }
    public void ChangeVolumeSFX(float volume)
    {
        mainAudioMixer.SetFloat("SFXVolume", volume);
        sfxVolumePercent = (int)volume + 80;
        sfxVolumeTMP.text = sfxVolumePercent + "%";
    }
}
