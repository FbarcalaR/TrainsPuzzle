using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateReloader : MonoBehaviour
{
    private Transform[,] instantiatedObjects;
    private static GameStateReloader gameStateReloader;

    private void Awake() {
        if(gameStateReloader == null) {
            gameStateReloader = this;
            DontDestroyOnLoad(gameStateReloader);
        }
        else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        LevelLoader.beginSceneReload += saveLevelState;
        SceneManager.sceneLoaded += reloadLevelState;
    }

    private void saveLevelState() {
        ObjectInstantiator objectInstantiator = FindObjectOfType<ObjectInstantiator>();
        if (objectInstantiator == null) return;

        instantiatedObjects = (Transform[,])objectInstantiator.instantiatedObjects.Clone();
        foreach (var obj in instantiatedObjects) {
            if(obj!=null)
                DontDestroyOnLoad(obj);
        }
    }

    private void reloadLevelState(Scene arg0, LoadSceneMode arg1) {
        ObjectInstantiator objectInstantiator = FindObjectOfType<ObjectInstantiator>();
        if(objectInstantiator != null)
            objectInstantiator.instantiated += reloadObjectInstantiatorState;
    }

    private void reloadObjectInstantiatorState(ObjectInstantiator objectInstantiator) { 
        if (objectInstantiator == null || instantiatedObjects == null) return;

        var objectToInstantiateBefore = objectInstantiator.objectToInstantiate;
        for (int x = 0; x < instantiatedObjects.GetLength(0); x++) {
            for (int y = 0; y < instantiatedObjects.GetLength(1); y++) {
                var obj = instantiatedObjects[x, y];
                if (obj == null) continue;

                var pos = new Vector2Int(x, y);
                objectInstantiator.SelectObjectToInstantiate(obj);
                objectInstantiator.AddRotation((int)obj.eulerAngles.z);
                objectInstantiator.InstantiatePrefab(obj, pos);
                objectInstantiator.AddRotation(-objectInstantiator.rotationOnZAxis);
                Destroy(obj.gameObject);
            }
        }

        objectInstantiator.SelectObjectToInstantiate(objectToInstantiateBefore);
        instantiatedObjects = null;
    }

}
