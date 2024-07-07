using TMPro;
using UnityEngine;

public class VideoSettingsPanel : UIElement
{
    [SerializeField] private SettingsMenuPanel _settingsMenuPanel;

    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    

    public override void Show()
    {
        foreach (var resolution in Screen.resolutions)
        {
            _resolutionDropdown.options.Add(new TMP_Dropdown.OptionData($"{resolution.width} x {resolution.height}"));
        }
        
        base.Show();
        
    }

    public void SetResolution(int index)
    {
        var resolution = Screen.resolutions[index];
        Screen.SetResolution(resolution.width,resolution.height,true);
    }

    public void ToggleFullscreen(bool state)
    {
        Screen.fullScreen = state;
    }
    public void Back()
    {
        Hide();
        _settingsMenuPanel.Show();
    }
}