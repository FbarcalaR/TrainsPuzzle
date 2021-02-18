using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour {

    public float linearSpeed = 1.5f;
    public float angularSpeed = 2;
    [Range(0, 1)]
    public float angularSpeedPercentageWhenTurning = 0.6f;
    public TrainRay leftRay;
    public TrainRay rightRay;

    private Rigidbody2D rigidbody2d;
    private float maxLeftRayDistance;
    private float maxRightRayDistance;
    public Train train;

    public void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        leftRay.railHitted += TurnLeft;
        rightRay.railHitted += TurnRight;
        maxLeftRayDistance = leftRay.maxRaycastDistance;
        maxRightRayDistance = rightRay.maxRaycastDistance;

        Physics2D.queriesStartInColliders = false;
        Physics2D.queriesHitTriggers = true;
    }

    private void FixedUpdate() {
        if (train.CanMove) {
            float turningVelocity = train.IsTurning() ? angularSpeedPercentageWhenTurning : 1;
            rigidbody2d.MovePosition(rigidbody2d.position + (Vector2)transform.right * Time.fixedDeltaTime * linearSpeed * turningVelocity);
        }
    }

    private void TurnLeft(RaycastHit2D rail) {
        if (train.CanMove) {
            rigidbody2d.MoveRotation(rigidbody2d.rotation + angularSpeed * Time.fixedDeltaTime / (rail.distance / maxLeftRayDistance));
        }
    }

    private void TurnRight(RaycastHit2D rail) {
        if (train.CanMove) {
            rigidbody2d.MoveRotation(rigidbody2d.rotation - angularSpeed * Time.fixedDeltaTime / (rail.distance / maxRightRayDistance));
        }
    }
}