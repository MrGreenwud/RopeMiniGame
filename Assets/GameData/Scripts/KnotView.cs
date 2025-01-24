using System.Collections.Generic;
using UnityEngine;

public class KnotView : MonoBehaviour
{
    [SerializeField] private KnotMover _knotMover;
    [SerializeField] private Sprite _knot;
    [SerializeField] private Sprite _knotHover;

    private Dictionary<Knot, SpriteRenderer> _spriteRenderers = new Dictionary<Knot, SpriteRenderer>();

    private void Awake()
    {
        Knot[] knots = FindObjectsByType<Knot>(FindObjectsSortMode.None);

        foreach(Knot knot in knots)
        {
            if (knot.TryGetComponent(out SpriteRenderer component) == false)
                continue;

            _spriteRenderers.Add(knot, component);
        }
    }

    public void UpdateView()
    {
        if (_knotMover.GrabbedKnot == null || _spriteRenderers.ContainsKey(_knotMover.GrabbedKnot) == false)
            return;

        foreach(SpriteRenderer renderer in _spriteRenderers.Values)
            renderer.sprite = _knot;

        _spriteRenderers[_knotMover.GrabbedKnot].sprite = _knotHover;
    }
}
