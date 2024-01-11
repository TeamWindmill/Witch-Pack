using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    [SerializeField] private Image artwork;
    [SerializeField] private Image circle;
    [SerializeField] private Transform pointer;

    private float time;
    private float counter;
    Transform target;

    public void InitIndicator(Transform target, Sprite artwork, float time = 0)
    {
        this.time = time;
        this.target = target;
        this.artwork.sprite = artwork;
        counter = 0f;
        circle.fillAmount = 1;
    }


    private void Update()
    {
        if (time > 0)
        {
            counter += GAME_TIME.GameDeltaTime;
            circle.fillAmount = counter / time;
            if (counter >= time)
            {
                gameObject.SetActive(false);
            }
        }

    }


}


