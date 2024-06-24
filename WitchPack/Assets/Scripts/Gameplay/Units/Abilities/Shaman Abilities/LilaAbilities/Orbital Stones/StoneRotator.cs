using System;
using UnityEngine;

public class StoneRotator : MonoBehaviour
{
    [SerializeField] private Transform pivotObject;
    [SerializeField] private float angularSpeed = 1f;
    [SerializeField] private float radius = 1f;
    [SerializeField] private float ElipseScale = 2;
 
    private float _lerpTimer;
    private float _currentAngle;
    private float posX, posY;

    private void Update()
    {
        _currentAngle += angularSpeed * Time.deltaTime;
        if (_currentAngle >= 360) _currentAngle = 0;
        
        posX = MathF.Sin(_currentAngle);
        posY = MathF.Cos(_currentAngle)/ElipseScale;
        var offset = new Vector3(posX,posY ,0) * radius;
        transform.position = pivotObject.position + offset;
    }
}