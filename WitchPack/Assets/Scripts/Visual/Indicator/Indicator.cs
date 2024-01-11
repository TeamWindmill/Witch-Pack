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

    public void InitIndicator(Transform target, Sprite artwork, float time)
    {
        this.time = time;
        this.target = target;
        this.artwork.sprite = artwork;
        counter = 0f;
        circle.fillAmount = 1;
        Vector2 midScreen = new Vector2(((RectTransform)transform.parent).sizeDelta.x * 0.5f, ((RectTransform)transform.parent).sizeDelta.y * 0.5f);
    }


    private void Update()
    {
        if (time != 0)//if timer is needed 
        {
            counter += GAME_TIME.GameDeltaTime;
            circle.fillAmount = counter / time;
            if (counter >= time)
            {
                gameObject.SetActive(false);
            }
        }
        //place object on screen edge (+some kind of offset) in the direction of the target
        //Vector2 dir = (target.position - transform.position).normalized;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //pointer.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //physikef
    }


}


