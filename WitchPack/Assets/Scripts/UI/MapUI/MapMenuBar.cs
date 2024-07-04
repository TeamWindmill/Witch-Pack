using System;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class MapMenuBar : UIElement
{

    [SerializeField] private Button[] _buttons;
    [SerializeField] private UIElement _upgradeScreen;
    [SerializeField] private UIElement _loreBookScreen;
    protected override void Awake()
    {
        base.Awake();
        SelectButton(0,false);
    }

    public void OpenMapScreen()
    {
        SelectButton(0);
        GameManager.CameraHandler.ToggleCameraLock(false);
        _upgradeScreen.Hide();
        _loreBookScreen.Hide();
    }
    public void OpenUpgradesScreen()
    {
        SelectButton(1);
        GameManager.CameraHandler.ToggleCameraLock(true);
        _upgradeScreen.Show();
        _loreBookScreen.Hide();
    }
    public void OpenLoreBookScreen()
    {
        SelectButton(2);
        GameManager.CameraHandler.ToggleCameraLock(true);
        _upgradeScreen.Hide();
        _loreBookScreen.Show();
    }
    private void SelectButton(int index,bool clickSound = true)
    {
        if(clickSound) SoundManager.PlayAudioClip(SoundEffectType.MenuClick);
        _buttons.ForEach(b => b.interactable = true);
        _buttons[index].interactable = false;
    }
}