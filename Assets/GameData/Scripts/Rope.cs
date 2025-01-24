using System;
using UnityEngine;

[Serializable]
public struct Rope 
{
    [SerializeField] private Knot _startKnot;
    [SerializeField] private Knot _endKnot;

    public Knot StartKnot => _startKnot;
    public Knot EndKnot => _endKnot;

    public Vector2 StartPosition => _startKnot.transform.position;
    public Vector2 EndPosition => _endKnot.transform.position;

    public Rope(Knot startKnot, Knot endKnot)
    {
        _startKnot = startKnot;
        _endKnot = endKnot;
    }
}
