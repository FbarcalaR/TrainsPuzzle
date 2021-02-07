using UnityEngine;

public abstract class Rail : MonoBehaviour
{
    public abstract bool CanMoveLeft();
    public abstract bool CanMoveRight();
    public abstract bool CanMoveUp();
    public abstract bool CanMoveDown();
    public bool[,] GetMovementMatrix() {
        var result = new bool[3, 3];
        result[1, 0] = CanMoveUp();
        result[0, 1] = CanMoveLeft();
        result[2, 1] = CanMoveRight();
        result[1, 2] = CanMoveDown();

        return result;
    }
}
