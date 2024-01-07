using UnityEngine;


public class EndScreenTimeUIHandler : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _text;


    public void UpdateUIVisual()
    {
        _text.text = $"{(int)(GAME_TIME.TimePlayed / 60)} : {GAME_TIME.TimePlayed % 60:00}";
    }
}