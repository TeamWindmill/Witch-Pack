using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class TutorialHandler : MonoSingleton<TutorialHandler>
{
    [SerializeField] public TutorialData MovementData;
    [SerializeField] public TutorialData PowerStructureData;
    [SerializeField] public TutorialData UpgradeData;

    [SerializeField] TextMeshProUGUI _tutorialText;
    [SerializeField] VideoPlayer _videoPlayer;
    [SerializeField] GameObject _tutorialCanvas;

    public void PlayMovementTutorial()
    {
        PlayTutorialVideo(MovementData);
    }
    public void PlayPowerStructuresTutorial()
    { 
        PlayTutorialVideo(PowerStructureData);
    }
    public void PlayUpgradeTutorial()
    {
        PlayTutorialVideo(UpgradeData);
    }
    private void PlayTutorialVideo(TutorialData data)
    {
        _tutorialText.text = data.TutorialText;
        _videoPlayer.clip = data.VideoClip;
        _tutorialCanvas.SetActive(true);
    }
}
