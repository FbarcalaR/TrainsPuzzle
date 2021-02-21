using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent GameWon;
    public UnityEvent GameOver;

    private int trainsOutOfTarget;
    void Start()
    {
        var allTrains = FindObjectsOfType<Train>();
        foreach (var train in allTrains) {
            train.trainsCollided += GameLost;
            train.trainInTarget += NewTrainInTarget;
            train.trainOutOfRails += GameLost;
        }
        trainsOutOfTarget = allTrains.Length;
    }

    private void NewTrainInTarget() {
        trainsOutOfTarget--;
        if(trainsOutOfTarget == 0) {
            GameWon?.Invoke();
        }
    }

    private void GameLost() {
        GameOver?.Invoke();
    }
}
