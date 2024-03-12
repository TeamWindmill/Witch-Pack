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
    [SerializeField] private GroundColliderTargeter groundTargeter;
    [SerializeField] private Targeter<Shadow> shadowTargeter;

    private float _spriteAlpha;


    public void Init(int id, float alpha)
    {
        _spriteAlpha = alpha;
        Id = id;
        groundTargeter.OnTargetAdded += OnShamanEnterTargeter;
        groundTargeter.OnTargetLost += OnShamanExitTargeter;
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
    private void OnShamanExitTargeter(GroundCollider obj) 
    {
        if (obj.Unit is Shaman shaman)
            OnShamanExit?.Invoke(Id,shaman);
    }
    private void OnShamanEnterTargeter(GroundCollider obj)
    {
        if (obj.Unit is Shaman shaman)
            OnShamanEnter?.Invoke(Id,shaman);
    }
    
    
}