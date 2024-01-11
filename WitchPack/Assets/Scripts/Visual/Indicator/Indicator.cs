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
        Vector3 midScreen = new Vector3(Screen.width / 2, Screen.height / 2);
        Vector2 dir = (target.position - transform.position).normalized;
        transform.position = midScreen * dir;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        pointer.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //physikef
    }


}


