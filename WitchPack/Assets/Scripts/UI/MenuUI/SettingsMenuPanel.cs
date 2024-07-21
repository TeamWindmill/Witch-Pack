using UnityEngine;

public class SettingsMenuPanel : UIElement
{
    [SerializeField] private SettingsWindow _settingsWindow;
    [SerializeField] private VideoSettingsPanel _videoSettingsPanel;
    [SerializeField] private AudioSettingsPanel _audioSettingsPanel;

    public void VideoSetting()
    {
        SoundManager.PlayAudioClip(SoundEffectType.MenuClick);
        Hide();
        _videoSettingsPanel.Show();
    }

    public void AudioSetting()
    {
        SoundManager.PlayAudioClip(SoundEffectType.MenuClick);
        Hide();
        _audioSettingsPanel.Show();
    }

    public void Back()
    {
        SoundManager.PlayAudioClip(SoundEffectType.MenuClick);
        _settingsWindow.Hide();
    }
}