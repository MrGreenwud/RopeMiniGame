using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RopeCollisionDetector : MonoBehaviour
{
    [SerializeField] private KnotMover _mover;
    [SerializeField] private RopeCollector _ropeCollector;

    [Space(10)]

    [SerializeField] private float _ropeThickness;
    [SerializeField] private float _knotThickness;

    private Dictionary<Rope, bool> _isCollision = new Dictionary<Rope, bool>();

    public UnityEvent OnCheckCollision;

    public bool GetCollision(Rope rope)
    {
        if (_isCollision.ContainsKey(rope) == false)
            return false;

        return _isCollision[rope];
    }

    private void Awake()
    {
        _ropeCollector.ForEach((rope) => { _isCollision.Add(rope, false); });
    }

    private void Start()
    {
        CheckAllRope();
    }

    public void CheckAllRope()
    {
        _ropeCollector.ForEach((rope) =>
        {
            bool isCollision = false;

            _ropeCollector.ForEach((rope2) =>
            {
                if (rope.Equals(rope2) == false && isCollision == false)
                    isCollision = CheckOverlap(rope, rope2);
            });

            _isCollision[rope] = isCollision;
        });

        OnCheckCollision?.Invoke();
    }

    private bool CheckOverlap(Rope a, Rope b)
    {
        Box2D boxA = GetRopeBox(a, _ropeThickness);
        Box2D boxB = GetRopeBox(b, _ropeThickness);

        return Box2D.Overlap(boxA, boxB) || CheckCross(a, b);
    }

    private bool CheckCross(Rope a, Rope b)
    {
        float v1 = GetDirection(a.StartPosition, a.EndPosition, b.StartPosition);
        float v2 = GetDirection(a.StartPosition, a.EndPosition, b.EndPosition);
        float v3 = GetDirection(b.StartPosition, b.EndPosition, a.StartPosition);
        float v4 = GetDirection(b.StartPosition, b.EndPosition, a.EndPosition);

        if (v1 * v2 < 0 && v3 * v4 < 0)
            return true;

        return false;
    }

    private Box2D GetRopeBox(Rope rope, float thickness)
    {
        Vector3 direction = rope.EndPosition - rope.StartPosition;
        Vector2 cross = Vector3.Cross(direction, Vector3.forward).normalized;
        Vector2 direction2D = direction.normalized;

        Box2D box2D = new Box2D();

        box2D.Vertex1 = rope.StartPosition + (direction2D * _knotThickness) + cross * (thickness / 2);
        box2D.Vertex2 = rope.StartPosition + (direction2D * _knotThickness) - cross * (thickness / 2);
        box2D.Vertex3 = rope.EndPosition - (direction2D * _knotThickness) - cross * (thickness / 2);
        box2D.Vertex4 = rope.EndPosition - (direction2D * _knotThickness) + cross * (thickness / 2);

        return box2D;
    }

    private float GetDirection(Vector2 a, Vector2 b, Vector2 c)
    {
        return (b.y - a.y) * (c.x - b.x) - (b.x - a.x) * (c.y - b.y);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_isCollision == null)
            return;

        foreach (Rope rope in _isCollision.Keys)
        {
            Gizmos.color = _isCollision[rope] == false ? Color.green : Color.red;

            Box2D box = GetRopeBox(rope, _ropeThickness);

            DrawBox(box);

            Gizmos.DrawLine(rope.StartPosition, rope.EndPosition);
        }
    }

    public void DrawBox(Box2D box)
    {
        Gizmos.DrawSphere(box.Vertex1, 0.1f);
        Gizmos.DrawSphere(box.Vertex2, 0.1f);
        Gizmos.DrawSphere(box.Vertex3, 0.1f);
        Gizmos.DrawSphere(box.Vertex4, 0.1f);

        Gizmos.DrawLine(box.Vertex1, box.Vertex2);
        Gizmos.DrawLine(box.Vertex2, box.Vertex3);
        Gizmos.DrawLine(box.Vertex3, box.Vertex4);
        Gizmos.DrawLine(box.Vertex4, box.Vertex1);
    }
#endif
}