using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SortLayerSetter : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer ??= GetComponent<SpriteRenderer>();
        _spriteRenderer.sortingOrder = (int)(transform.position.y * -1);
    }

}
