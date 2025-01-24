using System;
using UnityEngine;

public struct Box2D
{
    public Vector2 Vertex1;
    public Vector2 Vertex2;
    public Vector2 Vertex3;
    public Vector2 Vertex4;

    public Box2D(Vector2 vertex1, Vector2 vertex2, Vector2 vertex3, Vector2 vertex4)
    {
        Vertex1 = vertex1;
        Vertex2 = vertex2;
        Vertex3 = vertex3;
        Vertex4 = vertex4;
    }

    public static bool Overlap(Box2D a, Box2D b)
    {
        Vector2[] axes1 = GetAxes(a);
        Vector2[] axes2 = GetAxes(b);

        foreach (var axis in axes1)
        {
            var (min1, max1) = ProjectBoxOnAxis(a, axis);
            var (min2, max2) = ProjectBoxOnAxis(b, axis);

            if (max1 < min2 || max2 < min1)
                return false;
        }

        foreach (var axis in axes2)
        {
            var (min1, max1) = ProjectBoxOnAxis(a, axis);
            var (min2, max2) = ProjectBoxOnAxis(b, axis);

            if (max1 < min2 || max2 < min1)
                return false;
        }

        return true;
    }

    private static Vector2[] GetAxes(Box2D box)
    {
        return new Vector2[]
        {
            (box.Vertex2 - box.Vertex1).normalized,
            (box.Vertex3 - box.Vertex2).normalized,
            (box.Vertex4 - box.Vertex3).normalized,
            (box.Vertex1 - box.Vertex4).normalized
        };
    }

    private static (float min, float max) ProjectBoxOnAxis(Box2D box, Vector2 axis)
    {
        float proj1 = Vector2.Dot(box.Vertex1, axis);
        float proj2 = Vector2.Dot(box.Vertex2, axis);
        float proj3 = Vector2.Dot(box.Vertex3, axis);
        float proj4 = Vector2.Dot(box.Vertex4, axis);

        float min = Mathf.Min(Mathf.Min(proj1, proj2), Mathf.Min(proj3, proj4));
        float max = Mathf.Max(Mathf.Max(proj1, proj2), Mathf.Max(proj3, proj4));

        return (min, max);
    }
}