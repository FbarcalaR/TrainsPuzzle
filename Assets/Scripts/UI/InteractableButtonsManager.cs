using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableButtonsManager : MonoBehaviour {
    public List<Button> buttons;
    public CompletedLevelsManager completedLevelsManager;

    void Start() {
        for (int i = 2; i < buttons.Count; i++) {
            bool completed = completedLevelsManager.IsLevelCompleted(i);
            if (!completed) {
                buttons[i].interactable = false;
            }
        }
    }
}
