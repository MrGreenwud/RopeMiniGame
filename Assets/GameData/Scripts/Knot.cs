using UnityEngine;

public class Knot : MonoBehaviour 
{
    [SerializeField] private Vector2 _correctPosition;

    public Vector2 CorrectPosition => _correctPosition;
}