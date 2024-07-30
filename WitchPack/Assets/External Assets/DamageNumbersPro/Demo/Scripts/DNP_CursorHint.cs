using UnityEngine;

namespace External_Assets.DamageNumbersPro.Demo.Scripts
{
    public class DNP_CursorHint : MonoBehaviour
    {
        CanvasGroup cg;

        void Start()
        {
            cg = GetComponent<CanvasGroup>();
        }

        void FixedUpdate()
        {
            if(Cursor.visible)
            {
                cg.alpha = Mathf.Max(cg.alpha - Time.deltaTime * 2f, 0);
            }else
            {
                cg.alpha = Mathf.Min(cg.alpha + Time.deltaTime * 2f, 1);
            }
        }
    }
}
