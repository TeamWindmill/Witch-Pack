using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class LevelPath : MonoBehaviour
{
    public float PathDuration => _pathDuration;

    [SerializeField] private SpriteShapeRenderer _shapeRenderer;
    [SerializeField] private SpriteShapeController _shapeController;
    [SerializeField] private float _pathDuration;

    private void OnValidate()
    {
        _shapeRenderer ??= GetComponent<SpriteShapeRenderer>();
        _shapeController ??= GetComponent<SpriteShapeController>();
    }

    public void TogglePathMask(SpriteMaskInteraction state)
    {
        if (_shapeRenderer == null) return;
        _shapeRenderer.maskInteraction = state;
    }

    public Vector3[] GetPathPoints(Vector3 startPos)  
    {
        var controlPoints = _shapeController.spline.GetPointCount() - 2;

        List<Vector3> points = new();
        

        for (int i = 0; i < controlPoints; i++)
        {
            // if (i == controlPoints - 1) //last point in path
            // {
            //     points.Add(transform.position + _shapeController.spline.GetPosition(i) + _shapeController.spline.GetLeftTangent(i));
            //     points.Add(transform.position + _shapeController.spline.GetPosition(i));
            //     continue;
            // }

            if (i == 0)
            {
                points.Add(transform.position + _shapeController.spline.GetPosition(2));
                points.Add(startPos + _shapeController.spline.GetRightTangent(1)); //tangent of the first point
                points.Add( transform.position + _shapeController.spline.GetPosition(2) + _shapeController.spline.GetLeftTangent(2));
                //points.Add(transform.position +  _shapeController.spline.GetPosition(i) +_shapeController.spline.GetRightTangent(i));
                continue;
            }

            points.Add(transform.position + _shapeController.spline.GetPosition(i+ 2));
            points.Add(transform.position + _shapeController.spline.GetPosition(i +1) + _shapeController.spline.GetRightTangent(i + 1)); //tangent of the first point
            points.Add( transform.position + _shapeController.spline.GetPosition(i + 2) + _shapeController.spline.GetLeftTangent(i + 2));
            //points.Add(transform.position +  _shapeController.spline.GetPosition(i) +_shapeController.spline.GetRightTangent(i));
        }

        // for (int i = 0; i < points.Count; i++)
        // {
        //     Debug.Log($"{i}: {points[i]}");
        // }

        return points.ToArray();
    }

    [SerializeField] private Transform _startPos;
    [SerializeField] private float _debugPointRadius;
    

    private void OnDrawGizmosSelected()
    {
        if(_startPos == null) return;
        var pathPoints = GetPathPoints(_startPos.position);
        for (int i = 0; i < pathPoints.Length; i++)
        {
            if (i == 0 || i % 3 == 0)
            {
                Gizmos.color = Color.blue;
            }
            else if ((i - 1)%3 == 0)
            {
                Gizmos.color = Color.red;
            }
            else if ((i - 2)%3 == 0)
            {
                Gizmos.color = Color.yellow;
            }
            else
            {
                Gizmos.color = Color.black; 
            }
            
            Gizmos.DrawSphere(pathPoints[i],_debugPointRadius);
        }
    }
}