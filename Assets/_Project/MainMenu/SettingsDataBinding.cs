using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsDataBinding : MonoBehaviour
{
    private void Awake()
    {
        SetCurrentData();
    }


    public void OnBrightnessValueChanged(float value)
    {
        BindBrightness(value);
        PlayerPrefs.SetFloat("Brightness", value);
    }

    public void OnParticleSizeValueChanged(float value)
    {
        BindParticleSize(value);
        PlayerPrefs.SetFloat("ParticleSize", value);
    }

    public void OnVolumeMasterChanged(float value)
    {
        BindVolumeMaster(value);
        PlayerPrefs.SetFloat("VolumeMaster", value);
    }

    public void OnVolumeMusicChanged(float value)
    {
        BindVolumeMusic(value);
        PlayerPrefs.SetFloat("VolumeMusic", value);
    }
    
    public void OnVolumeSFXChanged(float value)
    {
        BindVolumeSFX(value);
        PlayerPrefs.SetFloat("VolumeSFX", value);
    }


    public static float BrightnessValue;
    private void BindBrightness(float value)
    {
        BrightnessValue = value;
    }
    
    public static float ParticleSizeValue;
    private void BindParticleSize(float value)
    {
        ParticleSizeValue = value;
    }
    [SerializeField] private AudioMixer mixer;
    private void BindVolumeMaster(float value)
    {
        float volume = Mathf.Log10(value) * 20;
        if (value == 0) volume = 0;
        mixer.SetFloat("VolumeMaster", volume);
    }
    private void BindVolumeMusic(float value)
    {
        float volume = Mathf.Log10(value) * 20;
        if (value == 0) volume = 0;
        mixer.SetFloat("VolumeMusic", volume);
    }
    private void BindVolumeSFX(float value)
    {
        float volume = Mathf.Log10(value) * 20;
        if (value == 0) volume = 0;
        mixer.SetFloat("VolumeSFX", volume);
    }



    private void SetCurrentData()
    {
        SetBrightnessSlider(PlayerPrefs.GetFloat("Brightness", 1f));
        SetParticleSizeSlider(PlayerPrefs.GetFloat("ParticleSize", 1f));
        SetVolumeMasterSlider(PlayerPrefs.GetFloat("VolumeMaster", 1f));
        SetVolumeMusicSlider(PlayerPrefs.GetFloat("VolumeMusic", 1f));
        SetVolumeSFXSlider(PlayerPrefs.GetFloat("VolumeSFX", 1f));
        UpdateHighscore(0);
        UpdateWinLossRatio();
    }

    public void OnWin(int profit)
    {
        int wins = PlayerPrefs.GetInt("Wins", 0);
        PlayerPrefs.SetInt("Wins", wins + 1);
        UpdateWinLossRatio();
        UpdateHighscore(profit);
    }

    public void OnLose()
    {
        int losses = PlayerPrefs.GetInt("Losses", 0);
        PlayerPrefs.SetInt("Losses", losses + 1);
        UpdateWinLossRatio();
    }
    

    [SerializeField] private Slider brightnessSlider;
    private void SetBrightnessSlider(float value)
    {
        brightnessSlider.value = value;
        BindBrightness(value);
    }
    [SerializeField] private Slider particleSizeSlider;
    private void SetParticleSizeSlider(float value)
    {
        particleSizeSlider.value = value;
        BindParticleSize(value);
    }
    
    [SerializeField] private Slider volumeMasterSlider;
    private void SetVolumeMasterSlider(float value)
    {
        volumeMasterSlider.value = value;
        BindVolumeMaster(value);
    }
    [SerializeField] private Slider volumeMusicSlider;
    private void SetVolumeMusicSlider(float value)
    {
        volumeMusicSlider.value = value;
        BindVolumeMusic(value);
    }
    [SerializeField] private Slider volumeSFXSlider;
    private void SetVolumeSFXSlider(float value)
    {
        volumeSFXSlider.value = value;
        BindVolumeSFX(value);
    }


    [SerializeField] private TMP_Text highscoreText;
    private void UpdateHighscore(int value)
    {
        int currentHighscore = PlayerPrefs.GetInt("Best", -1);
        if (value > currentHighscore)
        {
            PlayerPrefs.SetInt("Best", value);
            highscoreText.text = value.ToString();
        }
    }
    
    private void UpdateWinLossRatio()
    {
        int wins = PlayerPrefs.GetInt("Wins", 0);
        int losses = PlayerPrefs.GetInt("Losses", 0);
        int total = wins + losses;
        SetGamesPlayed(total);
        SetWinPercentValue(total <= 0 ? 0 : (float) wins / total);
    }
    
    

    [SerializeField] private TMP_Text gamesPlayedText;
    private void SetGamesPlayed(int value)
    {
        gamesPlayedText.text = value.ToString();
    }

    [SerializeField] private TMP_Text winPercentText;
    private void SetWinPercentValue(float percentage)
    {
        float winPercent = percentage * 100f;
        winPercentText.text = winPercent.ToString("F2") + "%";
    }

}
