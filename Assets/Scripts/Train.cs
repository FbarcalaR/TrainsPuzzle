using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Train : MonoBehaviour {
    public enum outterGridStartingPosition {up, down, left, right}
    public outterGridStartingPosition outPosition;
    public Vector2Int gridStartingPosition;
    public float trainSpeed = 1.5f;
    public ObjectInstantiator objectInstantiator;

    private List<PathPositionAngles> path;
    private Rigidbody2D rigidbody2d;
    private Vector3 targetPosition;
    private Vector2Int outGridStartingPosition;

    public bool CanMove { get; set; } = false;

    void Start() {
        path = new List<PathPositionAngles>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;

        switch (outPosition) {
            case outterGridStartingPosition.up:
                outGridStartingPosition = Vector2Int.up;
                break;
            case outterGridStartingPosition.down:
                outGridStartingPosition = Vector2Int.down;
                break;
            case outterGridStartingPosition.left:
                outGridStartingPosition = Vector2Int.left;
                break;
            case outterGridStartingPosition.right:
                outGridStartingPosition = Vector2Int.right;
                break;
            default:
                break;
        }
    }

    void Update() {
        if (CanMove && path.Count > 0 && TargetPositionReached()) {
            var positionAngles = path.First();
            path.RemoveAt(0);

            var nextPosition = objectInstantiator.instantiatedObjects[positionAngles.position.x, positionAngles.position.y].transform.position;

            Vector3 targetVelocity = (nextPosition - targetPosition) * 5f;
            targetVelocity.Normalize();
            rigidbody2d.velocity = targetVelocity * trainSpeed;
            rigidbody2d.rotation += positionAngles.angles;

            targetPosition = nextPosition;
        }

        if(path.Count == 0 && TargetPositionReached()) {
            CanMove = false;
            rigidbody2d.angularVelocity = 0;
            rigidbody2d.velocity = Vector3.zero;
        }
    }

    private bool TargetPositionReached() {
        return Vector3.Distance(targetPosition, transform.position) <= 0.1f;
    }

    public void UpdatePath() {
        Vector2Int lastPosition = gridStartingPosition + outGridStartingPosition;
        Vector2Int? currentPosition = gridStartingPosition;
        Vector2Int? nextPosition;
        Transform currentTransform = objectInstantiator.instantiatedObjects[gridStartingPosition.x, gridStartingPosition.y];
        float anglesToTurn = 0;

        while (IsPositionValidForPath(currentTransform) && currentPosition.HasValue) {
            path.Add(new PathPositionAngles(currentPosition.Value, anglesToTurn));

            nextPosition = GetNextPosition(currentPosition.Value, currentTransform.GetComponent<Rail>());
            anglesToTurn = GetAnglesDifference(lastPosition, currentPosition.Value, nextPosition);
            lastPosition = currentPosition.Value;
            currentPosition = nextPosition ?? null;
            currentTransform = currentPosition != null ? objectInstantiator.instantiatedObjects[currentPosition.Value.x, currentPosition.Value.y] : null;
        }
    }

    private float GetAnglesDifference(Vector2Int? lastPosition, Vector2Int currentPosition, Vector2Int? nextPosition) {
        if (!lastPosition.HasValue || !nextPosition.HasValue) return 0;
        Vector2Int firstVector = currentPosition - lastPosition.Value;
        Vector2Int secondVector = nextPosition.Value - currentPosition;
        float targetAngle = Vector2.SignedAngle(firstVector, secondVector);
        return targetAngle;
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

    private bool DirectionIsValid(Vector2Int nextPosition, Rail rail, Func<Rail, bool> func) {
        return func.Invoke(rail) 
            && IsMatrixPositionInBounds(nextPosition.x, nextPosition.y, objectInstantiator.instantiatedObjects)
            && !path.Select(p=>p.position).Contains(nextPosition);
    }

    private bool IsMatrixPositionInBounds(int x, int y, Transform[,] matrix) {
        return x >= 0 && y >= 0 && x < matrix.GetLength(0) && y < matrix.GetLength(1);
    }

    private struct PathPositionAngles {
        public Vector2Int position;
        public float angles;

        public PathPositionAngles(Vector2Int position, float angles) {
            this.position = position;
            this.angles = angles;
        }
    }

}
