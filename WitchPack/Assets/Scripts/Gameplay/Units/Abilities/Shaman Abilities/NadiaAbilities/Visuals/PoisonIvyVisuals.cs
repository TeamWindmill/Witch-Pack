using UnityEngine;

public class PoisonIvyVisuals : MonoBehaviour
{
    private float elapsedTime;
    private float duration;

    private ParticleSystem poisonIvyParticle;
    


    // Update is called once per frame
    void Update()
    {
        elapsedTime += GAME_TIME.GameDeltaTime;
        if(elapsedTime >= duration)
        {
            StopPoisonParticle();
        }
    }

    public void PlayPoisonParticle(float duration)
    {
        elapsedTime = 0;
        this.duration = duration;
        this.gameObject.SetActive(true);
    }

    public void StopPoisonParticle()
    {
        elapsedTime = 0;
        gameObject.SetActive(false);
    }


}
