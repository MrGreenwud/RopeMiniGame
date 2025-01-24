using System;
using UnityEngine;
using UnityEngine.Events;

public class KnotMover : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _knotLayer;

    private Knot _grabbedKnot;
    private Vector3 _moveOffset;
    private Vector2 _mousePosition;

    public Knot GrabbedKnot => _grabbedKnot;
    public bool IsDragging => _grabbedKnot != null;

    public UnityEvent OnDraggingChange;

    private void Update()
    {
        _mousePosition = GetTouchPositionOnWorld(InputHandler.TouchPosition());

        if (IsDragging == false && InputHandler.BeginTouchScreen())
        {
            if (TryGrab(out _grabbedKnot))
            {
                _moveOffset = _grabbedKnot.transform.position - 
                    new Vector3(_mousePosition.x, _mousePosition.y, 0);
                
                OnDraggingChange?.Invoke();
            }
        }
        else if (IsDragging && InputHandler.EndTouchScreen())
        {
            _grabbedKnot = null;
            OnDraggingChange?.Invoke();
        }

        if (IsDragging)
            Move();
    }

    public bool TryGrab(out Knot knot)
    {
        knot = null;

        RaycastHit2D hit = Physics2D.Raycast(_mousePosition, Vector2.zero, 1, _knotLayer);

        if (hit.collider == null || hit.collider.TryGetComponent(out knot) == false)
            return false;

        return true;
    }

    public void Move()
    {
        Vector3 newPosition = new Vector3(_mousePosition.x, _mousePosition.y, 0);
        _grabbedKnot.transform.position = newPosition + _moveOffset;
    }

    private Vector2 GetTouchPositionOnWorld(Vector2 touchPositionOnScreen)
    {
        Vector3 touchPositionOnWorld = _camera.ScreenToWorldPoint(touchPositionOnScreen);
        touchPositionOnWorld.z = 0;

        return new Vector2(touchPositionOnWorld.x, touchPositionOnWorld.y);
    }
}