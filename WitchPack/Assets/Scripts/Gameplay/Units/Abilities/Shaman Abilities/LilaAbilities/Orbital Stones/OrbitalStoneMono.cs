using System;
using Tools.Lerp;
using Unity.Mathematics;
using UnityEngine;

public class OrbitalStoneMono : MonoBehaviour
{
    [SerializeField] private Transform pivotObject;
    [SerializeField] private Vector3 _xlerpPos;
    [SerializeField] private Vector3 _ylerpPos;
    [SerializeField] private bool _lerpX;


    private float _lerpTimer;

    private SinLerper xLerp;
    private SinLerper yLerp;

    private void Start()
    {
        var position = pivotObject.position;
        xLerp = new SinLerper(position ,_xlerpPos,0);
        yLerp = new SinLerper(position ,_ylerpPos,1);
    }

    private void Update()
    {
        xLerp.Update();
        yLerp.Update();
        transform.position = _lerpX ? xLerp.Value : yLerp.Value;
    }
}