using System.Collections.Generic;
using UnityEngine;

public class RopeView : MonoBehaviour
{
    [SerializeField] private RopeCollector _ropeCollector;
    [SerializeField] private RopeCollisionDetector _ropeCollisionDetector;

    [Space(10)]

    [SerializeField] private GameObject _ropeRenderer;
    [SerializeField] private Color _collisionColor;
    [SerializeField] private Color _uncollisionColor;

    private Dictionary<Rope, LineRenderer> _lineRenderers = new Dictionary<Rope, LineRenderer>();

    private void Awake()
    {
        _ropeCollector.ForEach((rope) => 
        {
            GameObject ropeRenderer = Instantiate(_ropeRenderer, transform);
            LineRenderer renderer = ropeRenderer.GetComponent<LineRenderer>();
            _lineRenderers.Add(rope, renderer);
        });
    }

    private void Update()
    {
        foreach(Rope rope in _lineRenderers.Keys)
        {
            _lineRenderers[rope].SetPosition(0, rope.StartPosition);
            _lineRenderers[rope].SetPosition(1, rope.EndPosition);
        }
    }

    public void UpdateColor()
    {
        foreach (Rope rope in _lineRenderers.Keys)
        {
            bool isCollision = _ropeCollisionDetector.GetCollision(rope);
            Color color = isCollision ? _collisionColor : _uncollisionColor;
            _lineRenderers[rope].startColor = color;
            _lineRenderers[rope].endColor = color;
        }
    }
}
