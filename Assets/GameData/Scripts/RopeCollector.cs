using System;
using System.Collections.Generic;
using UnityEngine;

public class RopeCollector : MonoBehaviour
{
    [SerializeField] private Rope[] _ropes;

    public void ForEach(Action<Rope> action)
    {
        foreach (Rope rope in _ropes)
            action?.Invoke(rope);
    }
}