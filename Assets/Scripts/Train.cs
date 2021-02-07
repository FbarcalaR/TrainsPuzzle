using System;
using UnityEngine;

public class Train : MonoBehaviour {
    public bool CanMove = false;
    public ObjectInstantiator objectInstantiator;
    public GridMap grid;
    public Action<Transform[,], Vector2Int> railInstantiated;

    private bool[,] possibleMoves;
    // Start is called before the first frame update
    void Start() {
        possibleMoves = new bool[grid.mapSize.x, grid.mapSize.y];
        objectInstantiator.objectInstantiated += AddRail;
    }

    // Update is called once per frame
    void Update() {
        if (CanMove) {
        }
    }

    private void AddRail(Transform[,] railTransform, Vector2Int matrixPosition) {
        if (!railTransform[matrixPosition.x, matrixPosition.y].TryGetComponent<Rail>(out var rail)) return;

        var railMatrixMovement = rail.GetMovementMatrix();
        int possibleMovesXPosition = matrixPosition.x - 1;
        for (int x = 0; x < railMatrixMovement.GetLength(0); x++) {
            int possibleMovesYPosition = matrixPosition.y - 1;
            for (int y = 0; y < railMatrixMovement.GetLength(1); y++) {
                if (IsMatrixPositionInBounds(possibleMovesXPosition, possibleMovesYPosition, possibleMoves)) {
                    possibleMoves[possibleMovesXPosition, possibleMovesYPosition] =
                        (possibleMoves[possibleMovesXPosition, possibleMovesYPosition] || railMatrixMovement[x, y])
                        && railTransform[possibleMovesXPosition, possibleMovesYPosition] != null;
                }
                possibleMovesYPosition++;
            }
            possibleMovesXPosition++;
        }

        PrintMatrix();
    }

    private void PrintMatrix() {
        string debugMatrix = "";
        for (int x = possibleMoves.GetLength(0) - 1; x >= 0; x--) {
            for (int y = 0; y < possibleMoves.GetLength(1); y++) {
                debugMatrix += possibleMoves[y, x] + " ";
            }
            debugMatrix += "\n";
        }
        Debug.Log(debugMatrix);
    }

    private bool IsMatrixPositionInBounds(int x, int y, bool[,] matrix) {
        return x >= 0 && y >= 0 && x < matrix.GetLength(0) && y < matrix.GetLength(1);
    }
}
