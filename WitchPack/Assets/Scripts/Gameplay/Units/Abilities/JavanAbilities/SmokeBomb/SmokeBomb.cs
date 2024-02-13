using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SmokeBomb : MonoBehaviour
{
    [SerializeField] private PlayableDirector rangeEnter;
    [SerializeField] private PlayableDirector rangeExit;
    [SerializeField] private PlayableDirector cloudsEnter;
    [SerializeField] private PlayableDirector cloudsIdle;
    [SerializeField] private PlayableDirector cloudsExit;

    public void SpawnBomb(SmokeBombSO config)
    {
        rangeEnter.gameObject.SetActive(true);
        cloudsEnter.gameObject.SetActive(true);
        cloudsEnter.stopped += CloudsIdleAnim;
        transform.localScale = new Vector3(config.Range, config.Range, 0);
        Invoke(nameof(EndBomb),config.Duration);
    }

    private void CloudsIdleAnim(PlayableDirector clip)
    {
        clip.gameObject.SetActive(false);
        cloudsIdle.gameObject.SetActive(true);
    }
    

    private void EndBomb()
    {
        cloudsIdle.gameObject.SetActive(false);
        rangeEnter.gameObject.SetActive(false);
        cloudsExit.gameObject.SetActive(true);
        rangeExit.gameObject.SetActive(true);
        rangeExit.stopped += (p) =>
        {
            rangeExit.gameObject.SetActive(false);
            //gameObject.SetActive(false);
        };
    }
}
