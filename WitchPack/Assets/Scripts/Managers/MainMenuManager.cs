using System;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        BgMusicManager.Instance.PlayMusic(MusicClip.MenuMusic);
    }

    public void Play()
    {
        SoundManager.Instance.PlayAudioClip(SoundEffectType.MenuClick);
        GameManager.SceneHandler.LoadScene(SceneType.Map);
    }

    public void Quit()
    {
        SoundManager.Instance.PlayAudioClip(SoundEffectType.MenuClick);
        GameManager.Instance.Quit();
    }
}