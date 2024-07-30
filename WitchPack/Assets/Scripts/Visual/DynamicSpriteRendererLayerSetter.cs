using UnityEngine;

namespace Visual
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class DynamicSpriteRendererLayerSetter : MonoBehaviour
    {
        [SerializeField] private bool _updateInRuntime;
        private SpriteRenderer _spriteRenderer;
        void Start()
        {
            _spriteRenderer ??= GetComponent<SpriteRenderer>();
            _spriteRenderer.sortingOrder = (int)(transform.position.y * -100);
        }
        private void Update()
        {
            if (_updateInRuntime)
            {
                _spriteRenderer.sortingOrder = (int)(transform.position.y * -100);
            }

        }
    }
}
