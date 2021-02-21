using System;
using UnityEngine;

public class TrainOnRailRayCheck : MonoBehaviour {
    public LayerMask interactionMask;
    public float maxRaycastDistance = 0.2f;
    
    public Action railNotHitted;

    void FixedUpdate() {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, maxRaycastDistance, interactionMask);
        if (hitInfo.collider != null) {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
        }
        else {
            Debug.DrawLine(transform.position, transform.position + transform.right * maxRaycastDistance, Color.green);
            railNotHitted?.Invoke();
        }
    }
}
