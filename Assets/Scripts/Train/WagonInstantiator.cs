using UnityEngine;

public class WagonInstantiator : MonoBehaviour {
    public int numberOfWagons;
    public Train train;
    public GameObject wagonPrefab;
    public Vector3 distanceBetweenWagons = Vector3.right * -0.5f;
    public Vector3 offsetFromTrain = Vector3.right * -0.2f;
    public Vector3 offsetAngle = Vector3.zero;

    void Start()
    {
        InstanciateWagons();
    }

    private void InstanciateWagons() {
        var nextRbToAttach = train.rigidbody2d;

        for (int i = 1; i <= numberOfWagons; i++) {
            var instantiationPosition = transform.position + offsetFromTrain + distanceBetweenWagons * i;
            var newGameObject = Instantiate(wagonPrefab, instantiationPosition, Quaternion.Euler(offsetAngle));
            SetWagonProperties(newGameObject, nextRbToAttach);

            nextRbToAttach = newGameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void SetWagonProperties(GameObject newGameObject, Rigidbody2D nextRbToAttach) {
        var newWagon = newGameObject.GetComponent<Wagon>();
        newWagon.LinearSpeed = train.linearSpeed;
        newWagon.AngularSpeed = train.angularSpeed;
        newWagon.AngularSpeedPercentageWhenTurning = train.angularSpeedPercentageWhenTurning;
        newWagon.color = train.GetComponent<SpriteRenderer>().color;
        newWagon.Train = train;
        newGameObject.GetComponent<DistanceJoint2D>().connectedBody = nextRbToAttach;
    }
}
