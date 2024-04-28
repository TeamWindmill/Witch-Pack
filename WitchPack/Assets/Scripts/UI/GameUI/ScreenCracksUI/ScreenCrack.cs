using UnityEngine;
using UnityEngine.UI;

public class ScreenCrack : MonoBehaviour
{
    public ScreenCrackLerper ScreenCrackLerper => _screenCrackLerper;

    [SerializeField] private ScreenCrackLerper _screenCrackLerper;

    private void OnValidate()
    {
        if(_screenCrackLerper.Image is null)
            _screenCrackLerper.SetImage(GetComponent<Image>());
    }
}
