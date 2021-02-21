using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    public void LoadScene(int sceneNumber) {
        SceneManager.LoadScene(sceneNumber);
    }

    public void ReloadScene() {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.buildIndex);
    }
}
