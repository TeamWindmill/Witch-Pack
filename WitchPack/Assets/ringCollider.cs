using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class ringCollider : MonoBehaviour
{
    public int numberOfPoints = 360;
    public float radius = 1f;

    void OnValidate()
    {
        PolygonCollider2D polyCollider = GetComponent<PolygonCollider2D>();
        polyCollider.points = GenerateRingPoints();
    }

    
    Vector2[] GenerateRingPoints()
    {
        Vector2[] points = new Vector2[numberOfPoints];

        for (int i = 0; i < numberOfPoints; i++)
        {
            float angle = Mathf.Deg2Rad * (i / (float)numberOfPoints) * 360f;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            points[i] = new Vector2(x, y);
        }

        return points;
    }
}

