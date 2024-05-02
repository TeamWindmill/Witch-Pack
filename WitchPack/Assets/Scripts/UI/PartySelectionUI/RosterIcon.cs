using UnityEngine;
using UnityEngine.UI;

public class RosterIcon : UIElement
{
    [SerializeField] private Image _spriteRenderer;

    public Image SpriteRenderer => _spriteRenderer;
}