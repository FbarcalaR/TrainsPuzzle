using UnityEngine;

public class RailSelector : MonoBehaviour
{
    public GameObject firstDirection;
    public GameObject secondDirection;
    public Color colorOnDisabled;
    public Color colorOnEnabled;

    private bool canSwitch;
    private bool isFirstEnabled;

    private void Start() {
        Enable(firstDirection);
        Disable(secondDirection);
        isFirstEnabled = true;
        gameObject.GetComponent<Collider2D>().enabled = canSwitch;
    }

    public void SetCanSwitch(bool canSwitch) {
        this.canSwitch = canSwitch;
        gameObject.GetComponent<Collider2D>().enabled = canSwitch;
    }

    private void OnMouseDown() {
        if (canSwitch)
            SwitchDirections();
    }

    private void SwitchDirections() {
        if (isFirstEnabled) {
            Enable(secondDirection);
            Disable(firstDirection);
            isFirstEnabled = false;
        }
        else {
            Enable(firstDirection);
            Disable(secondDirection);
            isFirstEnabled = true;
        }
    }

    private void Enable(GameObject directionToEnable) {
        directionToEnable.transform.position -= Vector3.forward;
        directionToEnable.GetComponent<SpriteRenderer>().color = colorOnEnabled;
        directionToEnable.GetComponent<Collider2D>().enabled = true;
    }

    private void Disable(GameObject directionToDisable) {
        directionToDisable.transform.position += Vector3.forward;
        directionToDisable.GetComponent<SpriteRenderer>().color = colorOnDisabled;
        directionToDisable.GetComponent<Collider2D>().enabled = false;
    }
}
