﻿using UnityEngine;

namespace Tzipory.GameplayLogic.Managers.MainMenuMangers
{
    public class MainMenuManager : MonoBehaviour
    {
        public void Play()
        {
            GameManager.SceneHandler.LoadScene(SceneType.Map);
        }
    }
}