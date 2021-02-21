using TMPro;
using UnityEngine;

public class AvailableRailsDisplayer : MonoBehaviour
{
    public RailInstantiationRules railsInstantiationRules;
    public Transform railPrefabToDisplay;
    public TextMeshProUGUI text;

    private string originalText;

    private void Start() {
        originalText = text.text;
        text.text = originalText + railsInstantiationRules.GetAvailableRails(railPrefabToDisplay.tag);

        railsInstantiationRules.valueChanged += UpdateText;
    }

    private void UpdateText(string tag, int value) {
        if(tag == railPrefabToDisplay.tag) {
            text.text = originalText + value;
        }
    }
}
