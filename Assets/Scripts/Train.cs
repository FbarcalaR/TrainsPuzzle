using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Train : MonoBehaviour {
    public bool CanMove { get; set; } = false;
    public ObjectInstantiator objectInstantiator;

    private List<Vector2Int> path;
    private float timer = 0;


    // Start is called before the first frame update
    void Start() {
        path = new List<Vector2Int>();
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (CanMove && timer>=0.5f && path.Count > 0) {
            var position = path.First();
            path.RemoveAt(0);

            var newPosition = objectInstantiator.instantiatedObjects[position.x, position.y].transform.position;
            transform.position = newPosition;
            timer = 0;
        }
    }

    public void UpdatePath() {
        var currentPosition = new Vector2Int(0, 4);
        var currentTransform = objectInstantiator.instantiatedObjects[currentPosition.x, currentPosition.y];
        while (IsPositionValidForPath(currentTransform)) {
            path.Add(currentPosition);
            var nextPosition = GetNextPosition(currentPosition, currentTransform.GetComponent<Rail>());
            currentPosition = nextPosition.GetValueOrDefault();
            currentTransform = currentPosition != null ? objectInstantiator.instantiatedObjects[currentPosition.x, currentPosition.y] : null;
        }
    }

    private bool IsPositionValidForPath(Transform currentTransform) {
        return currentTransform != null
            && currentTransform.GetComponent<Rail>() != null;
    }

    private Vector2Int? GetNextPosition(Vector2Int currentPosition, Rail rail) {
        Vector2Int nextPosition = currentPosition;
        if (DirectionIsValid(currentPosition + Vector2Int.down, rail, r => r.CanMoveDown())) {
            return currentPosition + Vector2Int.down;
        }
        if (DirectionIsValid(currentPosition + Vector2Int.right, rail, r => r.CanMoveRight())) {
            return currentPosition + Vector2Int.right;
        }
        if (DirectionIsValid(currentPosition + Vector2Int.left, rail, r => r.CanMoveLeft())) {
            return currentPosition + Vector2Int.left;
        }
        if (DirectionIsValid(currentPosition + Vector2Int.up, rail, r => r.CanMoveUp())) {
            return currentPosition + Vector2Int.up;
        }
        return null;
    }

    private bool DirectionIsValid(Vector2Int nextPosition, Rail rail, Func<Rail, bool> p) {
        return p.Invoke(rail) 
            && IsMatrixPositionInBounds(nextPosition.x, nextPosition.y, objectInstantiator.instantiatedObjects)
            && !path.Contains(nextPosition);
    }

    private bool IsMatrixPositionInBounds(int x, int y, Transform[,] matrix) {
        return x >= 0 && y >= 0 && x < matrix.GetLength(0) && y < matrix.GetLength(1);
    }
}
