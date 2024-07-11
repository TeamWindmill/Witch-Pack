using UnityEngine;

public class SettingsMenuPanel : UIElement
{
    [SerializeField] private MainMenuPanel _mainMenuPanel;
    [SerializeField] private VideoSettingsPanel _videoSettingsPanel;
    [SerializeField] private AudioSettingsPanel _audioSettingsPanel;

    public void VideoSetting()
    {
        Hide();
        _videoSettingsPanel.Show();
    }

    public void AudioSetting()
    {
        Hide();
        _audioSettingsPanel.Show();
    }

    public void Back()
    {
        Hide();
        _mainMenuPanel.Show();
    }
}