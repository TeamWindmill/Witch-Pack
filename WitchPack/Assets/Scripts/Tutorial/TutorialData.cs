using UnityEngine;
using UnityEngine.Video;

namespace Tutorial
{
    [CreateAssetMenu(fileName = "Tutorial Data", menuName = "ScriptableObjects/TutorialConfig")]

    public class TutorialData : ScriptableObject
    {
        [SerializeField] private string _tutorialText;
        [SerializeField] private VideoClip _videoClip;

        public string TutorialText => _tutorialText;
        public VideoClip VideoClip => _videoClip;
    }
}
