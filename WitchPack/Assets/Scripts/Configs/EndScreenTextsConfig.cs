using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EndScreenTexts", menuName = "Configs/UIConfigs/EndScreenTexts", order = 0)]
    public class EndScreenTextsConfig : ScriptableObject
    {
        [SerializeField] private string[] _winText;
        [SerializeField] private string[] _loseText;

        public string[] WinText => _winText;

        public string[] LoseText => _loseText;
    }
}