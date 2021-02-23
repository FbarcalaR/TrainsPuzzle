using UnityEngine;

public class SetSwitchRailsActive : MonoBehaviour
{
    public void SwitchOnAllRails() {
        var allRails = FindObjectsOfType<RailSelector>();
        foreach (var rail in allRails) {
            rail.SetCanSwitch(true);
        }
    }
}
