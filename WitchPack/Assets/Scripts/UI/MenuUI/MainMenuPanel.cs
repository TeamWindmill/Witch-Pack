using System;
using UnityEngine;

public class MainMenuPanel : UIElement
{
    [SerializeField] private SettingsMenuPanel _settingsMenuPanel;

    private void Start()
    {
        BgMusicManager.Instance.PlayMusic(MusicClip.MenuMusic);
    }

    public void Play()
    {
        SoundManager.PlayAudioClip(SoundEffectType.MenuClick);
        GameManager.SceneHandler.LoadScene(SceneType.Map);
    }
    public void SettingsMenu()
    {
        Hide();
        _settingsMenuPanel.Show();
    }
    
    public void Quit()
    {
        SoundManager.PlayAudioClip(SoundEffectType.MenuClick);
        GameManager.Instance.Quit();
    }
}