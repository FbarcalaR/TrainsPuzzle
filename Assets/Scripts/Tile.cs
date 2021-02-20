using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public Color colorOnMouseOver;

    [HideInInspector]
    public Vector2Int positionInMatrix;
    public Action<Transform, Vector2Int> leftMouseClick;
    public Action<Transform, Vector2Int> rightMouseClick;
    public Action<Transform, Vector2Int> mouseEnter;
    public Action<Transform, Vector2Int> mouseExit;

    private Color originalColor;

    private void Start() {
        originalColor = SpriteRenderer.color;
    }

    private void OnMouseEnter() {
        mouseEnter?.Invoke(transform, positionInMatrix);
        SpriteRenderer.color = colorOnMouseOver;
    }

    private void OnMouseExit() {
        mouseExit?.Invoke(transform, positionInMatrix);
        SpriteRenderer.color = originalColor;
    }

    private void OnMouseDown() {
        leftMouseClick?.Invoke(transform, positionInMatrix);
    }

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(1)) {
            rightMouseClick?.Invoke(transform, positionInMatrix);
        }
    }
}
