using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tzipory.GameplayLogic.Managers.MainGameManagers
{
    public class PauseMenuManager : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

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
                    OpenPauseMenu();
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
            _canvas.gameObject.SetActive(false);
            GAME_TIME.Play();
            _isOpen = false;
        }

        public void ReturnToMap()
        {
            GameManager.SceneHandler.LoadScene(SceneType.Map);   
        }
    }
}
