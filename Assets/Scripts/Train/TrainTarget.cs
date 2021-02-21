using UnityEngine;

public class TrainTarget : MonoBehaviour
{
    public Train train;

    private void Start() {
        var trainColor = train.gameObject.GetComponent<SpriteRenderer>().color;
        gameObject.GetComponent<SpriteRenderer>().color = trainColor;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == train.gameObject) {
            train.SetTrainAsInTarget();
        }
    }
}
