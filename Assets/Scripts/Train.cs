using UnityEngine;

public class Train : MonoBehaviour {

    public float linearSpeed = 1.5f;
    public float angularSpeed = 2;
    [Range(0, 1)]
    public float angularSpeedPercentageWhenTurning = 0.6f;
    public TrainRay leftRay;
    public TrainRay rightRay;
    public int numberOfWagons;
    public GameObject wagonPrefab;

    private Rigidbody2D rigidbody2d;
    private float maxLeftRayDistance;
    private float maxRightRayDistance;
    private bool isTurningLeft = false;
    private bool isTurningRight = false;

    public bool CanMove { get; set; }

    public void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        leftRay.railHitted += TurnLeft;
        rightRay.railHitted += TurnRight;
        leftRay.railNotHitted += () => isTurningLeft = false;
        rightRay.railNotHitted += () => isTurningRight = false;
        maxLeftRayDistance = leftRay.maxRaycastDistance;
        maxRightRayDistance = rightRay.maxRaycastDistance;

        Physics2D.queriesStartInColliders = false;
        Physics2D.queriesHitTriggers = true;
        InstanciateWagons();
    }


    private void FixedUpdate() {
        if (CanMove) {
            float turningVelocity = IsTurning() ? angularSpeedPercentageWhenTurning : 1;
            rigidbody2d.MovePosition(rigidbody2d.position + (Vector2)transform.right * Time.fixedDeltaTime * linearSpeed * turningVelocity);
        }
    }

    private void InstanciateWagons() {
        var offset = Vector3.right * -0.5f;
        var nextRbToAttach = rigidbody2d;

        for (int i = 1; i <= numberOfWagons; i++) {
            var newGameObject = Instantiate(wagonPrefab, transform.position + offset * i, Quaternion.identity);
            var newWagon = newGameObject.GetComponent<Wagon>();
            newWagon.linearSpeed = this.linearSpeed;
            newWagon.angularSpeed = this.angularSpeed;
            newWagon.angularSpeedPercentageWhenTurning = this.angularSpeedPercentageWhenTurning;
            newWagon.train = this;
            newGameObject.GetComponent<DistanceJoint2D>().connectedBody = nextRbToAttach;
            nextRbToAttach = newGameObject.GetComponent<Rigidbody2D>();
        }
    }

    public bool IsTurning() {
        return isTurningLeft || isTurningRight;
    }

    private void TurnLeft(RaycastHit2D rail) {
        if (CanMove) {
            rigidbody2d.MoveRotation(rigidbody2d.rotation + angularSpeed * Time.fixedDeltaTime / (rail.distance / maxLeftRayDistance));
            isTurningLeft = true;
        }
    }

    private void TurnRight(RaycastHit2D rail) {
        if (CanMove) {
            rigidbody2d.MoveRotation(rigidbody2d.rotation - angularSpeed * Time.fixedDeltaTime / (rail.distance / maxRightRayDistance));
            isTurningRight = true;
        }
    }
}
