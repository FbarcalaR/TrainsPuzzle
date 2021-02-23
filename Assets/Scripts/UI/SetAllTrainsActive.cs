using UnityEngine;

public class SetAllTrainsActive : MonoBehaviour
{
    public void AllTrainsCanMove() {
        var allTrains = FindObjectsOfType<Train>();
        foreach (var train in allTrains) {
            train.CanMove = true;
        }
    }
}
