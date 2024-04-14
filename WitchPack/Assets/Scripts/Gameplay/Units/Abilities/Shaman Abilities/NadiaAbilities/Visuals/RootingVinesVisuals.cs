using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RootingVinesVisuals : MonoBehaviour
{
    [SerializeField] StatusEffectConfig root;
    float elapsedTime;
    [SerializeField] PlayableDirector exitAnimation;
    float exitTime;

    [SerializeField] private GameObject entryGameObject;
    [SerializeField] private GameObject exitGameObject;

    private void InitializeState()
    {
        elapsedTime = 0;
        SwitchGameObjects(true);
    }

    private void Start()
    {
        exitTime = root.Duration - (float)exitAnimation.duration;
        InitializeState();
    }

    private void OnDisable()
    {
        InitializeState();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += GAME_TIME.GameDeltaTime;
        if (elapsedTime >= exitTime)
        {
            SwitchGameObjects(false);
        }
    }

    private void SwitchGameObjects(bool entryState)
    {
        entryGameObject.SetActive(entryState);
        exitGameObject.SetActive(!entryState);
    }
}
