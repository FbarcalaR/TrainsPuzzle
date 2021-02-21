using UnityEngine;

public class Wagon : MonoBehaviour {
    public RailFollowRay leftRay;
    public RailFollowRay rightRay;

    private Rigidbody2D rigidbody2d;
    private float maxLeftRayDistance;
    private float maxRightRayDistance;

    public float LinearSpeed { get; set; }
    public float AngularSpeed { get; set; }
    public float AngularSpeedPercentageWhenTurning { get; set; }
    public Train Train { get; set; }
    public Color color { get; set; }

    public void Awake() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        leftRay.railHitted += TurnLeft;
        rightRay.railHitted += TurnRight;
        maxLeftRayDistance = leftRay.maxRaycastDistance;
        maxRightRayDistance = rightRay.maxRaycastDistance;

        Physics2D.queriesStartInColliders = false;
        Physics2D.queriesHitTriggers = true;
    }

    public void Start() {
        if (gameObject.GetComponent<SpriteRenderer>() != null)
            gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    private void FixedUpdate() {
        if (Train.CanMove) {
            float turningVelocity = Train.IsTurning() ? AngularSpeedPercentageWhenTurning : 1;
            rigidbody2d.MovePosition(rigidbody2d.position + (Vector2)transform.right * Time.fixedDeltaTime * LinearSpeed * turningVelocity);
        }
    }

    private void TurnLeft(RaycastHit2D rail) {
        if (Train.CanMove) {
            rigidbody2d.MoveRotation(rigidbody2d.rotation + AngularSpeed * Time.fixedDeltaTime / (rail.distance / maxLeftRayDistance));
        }
    }

    private void TurnRight(RaycastHit2D rail) {
        if (Train.CanMove) {
            rigidbody2d.MoveRotation(rigidbody2d.rotation - AngularSpeed * Time.fixedDeltaTime / (rail.distance / maxRightRayDistance));
        }
    }
}