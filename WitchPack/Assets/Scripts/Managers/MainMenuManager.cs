using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Transform _menuPanel;
    [SerializeField] private Transform _settingsPanel;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;
    private void Start()
    {
        BgMusicManager.Instance.PlayMusic(MusicClip.MenuMusic);
        _menuPanel.gameObject.SetActive(true);
        _settingsPanel.gameObject.SetActive(false);
    }

    public void Play()
    {
        SoundManager.Instance.PlayAudioClip(SoundEffectType.MenuClick);
        GameManager.SceneHandler.LoadScene(SceneType.Map);
    }

    public void OpenSettingsMenu()
    {
        _menuPanel.gameObject.SetActive(false);
        _settingsPanel.gameObject.SetActive(true);
        if (_audioMixer.GetFloat("MusicVolume", out var musicVolume))
        {
            _musicVolumeSlider.value = musicVolume;
        }
        if (_audioMixer.GetFloat("SFXVolume", out var sfxVolume))
        {
            _sfxVolumeSlider.value = sfxVolume;
        }
    }
    public void CloseSettingsMenu()
    {
        _menuPanel.gameObject.SetActive(true);
        _settingsPanel.gameObject.SetActive(false);
    }

    public void ChangeMusicVolume(float value) => _audioMixer.SetFloat("MusicVolume", value);
    public void ChangeSFXVolume(float value) => _audioMixer.SetFloat("SFXVolume", value);
    

    public void Quit()
    {
        SoundManager.Instance.PlayAudioClip(SoundEffectType.MenuClick);
        GameManager.Instance.Quit();
    }
}