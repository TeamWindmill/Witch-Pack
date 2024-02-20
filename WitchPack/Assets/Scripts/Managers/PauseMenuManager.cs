using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Transform mapButton;
    [SerializeField] private TextMeshProUGUI pauseTitleText;

    private bool _isOpen;
    private bool _isSkippedFrame;

    private void Awake()
    {
        _canvas.gameObject.SetActive(false);
        _isOpen = false;
        _isSkippedFrame = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !SceneHandler.IsLoading && !_isOpen)
        {
            var scene = SceneManager.GetActiveScene();
            if (scene.buildIndex == (int)SceneType.Game)
            {
                pauseTitleText.text = "game paused";
                mapButton.gameObject.SetActive(true);
                OpenPauseMenu();
            }
            else if (scene.buildIndex == (int)SceneType.Map)
            {
                pauseTitleText.text = "witch pack";
                mapButton.gameObject.SetActive(false);
                OpenPauseMenu();
            }
        }

        if (!_isOpen)
            return;

        if (!_isSkippedFrame)
        {
            _isSkippedFrame = true;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && _isSkippedFrame && _isOpen)
            Resume();
    }

    private void OpenPauseMenu()
    {
        _isOpen = true;
        _isSkippedFrame = false;
        _canvas.gameObject.SetActive(true);
        GAME_TIME.Pause();
    }

    public void Resume()
    {
        SoundManager.Instance.PlayAudioClip(SoundEffectType.MenuClick);
        _canvas.gameObject.SetActive(false);
        GAME_TIME.Play();
        _isOpen = false;
    }

    public void ReturnToMap()
    {
        SoundManager.Instance.PlayAudioClip(SoundEffectType.MenuClick);
        BgMusicManager.Instance.PlayMusic(MusicClip.MenuMusic);
        _canvas.gameObject.SetActive(false);
        GameManager.SceneHandler.LoadScene(SceneType.Map);
    }

    public void Quit()
    {
        BgMusicManager.Instance.StopMusic();
        SoundManager.Instance.PlayAudioClip(SoundEffectType.MenuClick);
        GameManager.Instance.Quit();
    }
}