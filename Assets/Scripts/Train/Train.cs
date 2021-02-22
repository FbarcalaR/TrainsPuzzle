using System;
using UnityEngine;

public class Train : MonoBehaviour {

    public float linearSpeed = 1.5f;
    public float angularSpeed = 2;
    [Range(0, 1)]
    public float angularSpeedPercentageWhenTurning = 0.6f;
    public bool moveOnAwake = false;
    public RailFollowRay leftRay;
    public RailFollowRay rightRay;
    public TrainOnRailRayCheck trainOnRailRayCheck;
    public Action trainsCollided;
    public Action trainOutOfRails;
    public Action trainInTarget;

    [HideInInspector]
    public Rigidbody2D rigidbody2d;

    private float maxLeftRayDistance;
    private float maxRightRayDistance;
    private bool isTurningLeft = false;
    private bool isTurningRight = false;

    public bool CanMove { get; set; }

    public void Awake() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        CanMove = moveOnAwake;
        leftRay.railHitted += TurnLeft;
        rightRay.railHitted += TurnRight;
        trainOnRailRayCheck.railNotHitted += TrainIsOutOfRails;
        leftRay.railNotHitted += () => isTurningLeft = false;
        rightRay.railNotHitted += () => isTurningRight = false;
        maxLeftRayDistance = leftRay.maxRaycastDistance;
        maxRightRayDistance = rightRay.maxRaycastDistance;

        Physics2D.queriesStartInColliders = false;
        Physics2D.queriesHitTriggers = true;
    }

    private void FixedUpdate() {
        if (CanMove) {
            float turningVelocity = IsTurning() ? angularSpeedPercentageWhenTurning : 1;
            rigidbody2d.MovePosition(rigidbody2d.position + (Vector2)transform.right * Time.fixedDeltaTime * linearSpeed * turningVelocity);
        }
    }

    public bool IsTurning() {
        return isTurningLeft || isTurningRight;
    }

    public void SetTrainAsInTarget() {
        CanMove = false;
        trainInTarget?.Invoke();
    }

    private void TrainIsOutOfRails() {
        CanMove = false;
        trainOutOfRails?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        collision.gameObject.TryGetComponent<Wagon>(out var wagon);
        if (wagon?.Train == this) return;

        if (collision.gameObject.layer == gameObject.layer) {
            CanMove = false;
            trainsCollided?.Invoke();
        }
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
