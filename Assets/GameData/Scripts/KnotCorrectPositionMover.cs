using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnotCorrectPositionMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private bool _isMove;
    private float _t;

    private Dictionary<Transform, (Vector2, Vector2)> _correctPositions = new Dictionary<Transform, (Vector2, Vector2)>();

    public UnityEvent OnMoved;

    private void Update()
    {
        UpdatePositions();
    }

    public void Move()
    {
        _isMove = true;

        Knot[] knots = FindObjectsByType<Knot>(FindObjectsSortMode.None);

        foreach (Knot knot in knots)
            _correctPositions.Add(knot.transform, (knot.transform.position, knot.CorrectPosition));
    }

    private void UpdatePositions()
    {
        if (_isMove == false)
            return;

        _t += Time.deltaTime * _speed;

        foreach (Transform transform in _correctPositions.Keys)
            transform.position = Vector3.Lerp(_correctPositions[transform].Item1, _correctPositions[transform].Item2, _t);

        if (_t < 1)
            return;

        _isMove = false;
        OnMoved?.Invoke();
    }
}
