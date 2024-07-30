using Sound;
using UI.UISystem;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI.MenuUI
{
    public class AudioSettingsPanel : UIElement
    {
        [SerializeField] private SettingsMenuPanel _settingsMenuPanel;
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _sfxVolumeSlider;

        public override void Show()
        {
            base.Show();
            if (_audioMixer.GetFloat("MusicVolume", out var musicVolume))
            {
                _musicVolumeSlider.value = musicVolume;
            }

            if (_audioMixer.GetFloat("SFXVolume", out var sfxVolume))
            {
                _sfxVolumeSlider.value = sfxVolume;
            }
        }

        public void Back()
        {
            SoundManager.PlayAudioClip(SoundEffectType.MenuClick);
            Hide();
            _settingsMenuPanel.Show();
        }
        public void ChangeMusicVolume(float value) => _audioMixer.SetFloat("MusicVolume", value);
        public void ChangeSFXVolume(float value) => _audioMixer.SetFloat("SFXVolume", value);
    }
}