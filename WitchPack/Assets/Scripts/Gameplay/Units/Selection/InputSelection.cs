using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputSelection : MonoBehaviour
{
    private TimeButtons _lastTimeButton;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(LevelManager.Instance.ShamanParty.Count < 1) return;
            LevelManager.Instance.SelectionHandler.OnShamanClick(PointerEventData.InputButton.Left,LevelManager.Instance.ShamanParty[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(LevelManager.Instance.ShamanParty.Count < 2) return;
            LevelManager.Instance.SelectionHandler.OnShamanClick(PointerEventData.InputButton.Left,LevelManager.Instance.ShamanParty[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(LevelManager.Instance.ShamanParty.Count < 3) return;
            LevelManager.Instance.SelectionHandler.OnShamanClick(PointerEventData.InputButton.Left,LevelManager.Instance.ShamanParty[2]);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!Mathf.Approximately(GAME_TIME.TimeRate,0))
            {
                _lastTimeButton = TimeControlUIHandler.Instance.CurrentTimeButton.ButtonType;
                TimeControlUIHandler.Instance.ChangeTimeButton(TimeButtons.Pause);
            }
            else
            {
                TimeControlUIHandler.Instance.ChangeTimeButton(_lastTimeButton);
            }
        }
    }
}