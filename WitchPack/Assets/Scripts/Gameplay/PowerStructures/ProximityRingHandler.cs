using System;
using UnityEngine;


public class ProximityRingHandler : MonoBehaviour
{
    public event Action<int, Shadow> OnShadowEnter;
    public event Action<int, Shadow> OnShadowExit;
    public event Action<int, Shaman> OnShamanEnter;
    public event Action<int, Shaman> OnShamanExit;
    public int Id { get; private set; }

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Targeter<Shaman> shamanTargeter;
    [SerializeField] private Targeter<Shadow> shadowTargeter;

    private float _spriteAlpha;


    public void Init(int id, float alpha)
    {
        _spriteAlpha = alpha;
        Id = id;
        shamanTargeter.OnTargetAdded += OnShamanEnterTargeter;
        shamanTargeter.OnTargetLost += OnShamanExitTargeter;
        shadowTargeter.OnTargetAdded += OnShadowEnterTargeter;
        shadowTargeter.OnTargetLost += OnShadowExitTargeter;
    }

    

    public void Scale(float range)
    {
        transform.localScale = new Vector3(range, range, transform.localScale.z);
    }

    public void ToggleSprite(bool state)
    {
        spriteRenderer.enabled = state;
    }

    public void ChangeColor(Color color)
    {
        color.a = _spriteAlpha;
        spriteRenderer.color = color;
    }
    private void OnShadowExitTargeter(Shadow obj) => OnShadowExit?.Invoke(Id,obj);
    private void OnShadowEnterTargeter(Shadow obj) =>OnShadowEnter?.Invoke(Id,obj);
    private void OnShamanExitTargeter(Shaman obj) =>OnShamanExit?.Invoke(Id,obj);
    private void OnShamanEnterTargeter(Shaman obj) =>OnShamanEnter?.Invoke(Id,obj);
    
    
}