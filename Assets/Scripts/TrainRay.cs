using System;
using UnityEngine;

public class TrainRay : MonoBehaviour {

    public LayerMask interactionMask;
    public float maxRaycastDistance = 3;

    public Action<RaycastHit2D> railHitted;
    public Action railNotHitted;


    void FixedUpdate() {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, maxRaycastDistance, interactionMask);
        if (hitInfo.collider != null) {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            railHitted?.Invoke(hitInfo);
        }
        else {
            Debug.DrawLine(transform.position, transform.position + transform.right * maxRaycastDistance, Color.green);
            railNotHitted?.Invoke();
        }
    }

}
