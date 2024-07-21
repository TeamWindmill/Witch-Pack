using TMPro;
using UnityEngine;

public class VideoSettingsPanel : UIElement
{
    [SerializeField] private SettingsMenuPanel _settingsMenuPanel;

    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    

    public override void Show()
    {
        int currentResolutionIndex = -1;
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            _resolutionDropdown.options.Add(new TMP_Dropdown.OptionData($"{Screen.resolutions[i].width} x {Screen.resolutions[i].height}"));
            if (Screen.resolutions[i].height == Screen.currentResolution.height &&
                Screen.resolutions[i].width == Screen.currentResolution.width) currentResolutionIndex = i;
        }

        _resolutionDropdown.value = currentResolutionIndex;
        base.Show();
        
    }

    public void SetResolution(int index)
    {
        var resolution = Screen.resolutions[index];
        Screen.SetResolution(resolution.width,resolution.height,true);
    }

    public void ToggleFullscreen(bool state)
    {
        SoundManager.PlayAudioClip(SoundEffectType.MenuClick);
        Screen.fullScreen = state;
    }
    public void Back()
    {
        SoundManager.PlayAudioClip(SoundEffectType.MenuClick);
        Hide();
        _settingsMenuPanel.Show();
    }
}