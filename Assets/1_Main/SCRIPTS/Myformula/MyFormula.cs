using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MyFormula
{
public static (Vector2Int, Vector2Int) FindFarthestPoints(IEnumerable<Vector2Int> points)
    {
        if (points == null || points.Count() < 2)
        {
            throw new ArgumentException("The HashSet must contain at least two points.");
        }

        Vector2Int pointA = Vector2Int.zero;
        Vector2Int pointB = Vector2Int.zero;
        float maxDistance = float.MinValue;
        foreach (var p1 in points)
        {
            foreach (var p2 in points)
            {
                if (p1 == p2) continue;
                float distance = (p1 -p2).magnitude;
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    pointA = p1;
                    pointB = p2;
                }
            }
        }
        return (pointA, pointB);
    }
}
