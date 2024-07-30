using Managers;
using Sound;
using Tools.Scene;
using UI.UISystem;

namespace UI.MenuUI
{
    public class MainMenuPanel : UIElement
    {
        //[SerializeField] private SettingsMenuPanel _settingsMenuPanel;

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
            SoundManager.PlayAudioClip(SoundEffectType.MenuClick);
            UIManager.ShowUIGroup(UIGroup.SettingsWindow);
        }
    
        public void Quit()
        {
            SoundManager.PlayAudioClip(SoundEffectType.MenuClick);
            GameManager.Instance.Quit();
        }
    }
}