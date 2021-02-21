using UnityEngine;

public class ObjectInstantiator : MonoBehaviour {
    public GridMap grid;
    public Transform objectToInstantiate;
    [Range(0, 255)]
    public int aplhaPreshow = 130;
    public int rotationOnZAxis = 0;
    public int availableStraightRails;
    public RailInstantiationRules railsInstantiationRules;

    [HideInInspector]
    public Transform[,] instantiatedObjects;
    private Transform objectOnPreshow;

    public void Start() {
        grid.tileLeftClicked += InstantiatePrefab;
        grid.tileRightClicked += DestroyPrefab;
        grid.mouseEntersTile += StartPreshowPrefab;
        grid.mouseExitsTile += StopPreshowPrefab;
        instantiatedObjects = new Transform[grid.mapSize.x, grid.mapSize.y];
    }

    public void SelectObjectToInstantiate(Transform objectToInstantiate) {
        this.objectToInstantiate = objectToInstantiate;
    }

    public void AddRotation(int eulerAngles) {
        rotationOnZAxis += eulerAngles;
    }

    private void InstantiatePrefab(Transform tileTransform, Vector2Int matrixPosition) {
        if (railsInstantiationRules.GetAvailableRails(objectToInstantiate.tag) <= 0) return;

        if (instantiatedObjects[matrixPosition.x, matrixPosition.y] != null) {
            railsInstantiationRules.RailDeleted(instantiatedObjects[matrixPosition.x, matrixPosition.y].tag);
            Destroy(instantiatedObjects[matrixPosition.x, matrixPosition.y].gameObject);
        }
        instantiatedObjects[matrixPosition.x, matrixPosition.y] = Instantiate(objectToInstantiate, tileTransform.position, Quaternion.identity);
        instantiatedObjects[matrixPosition.x, matrixPosition.y].Rotate(Vector3.forward * rotationOnZAxis);
        railsInstantiationRules.RailInstantiated(objectToInstantiate.tag);
    }
    private void DestroyPrefab(Transform tileTransform, Vector2Int matrixPosition) {
        if (instantiatedObjects[matrixPosition.x, matrixPosition.y] == null) return;

        railsInstantiationRules.RailDeleted(instantiatedObjects[matrixPosition.x, matrixPosition.y].tag);
        Destroy(instantiatedObjects[matrixPosition.x, matrixPosition.y].gameObject);
    }

    private void StartPreshowPrefab(Transform tileTransform, Vector2Int matrixPosition) {
        objectOnPreshow = Instantiate(objectToInstantiate, tileTransform.position, Quaternion.identity);
        objectOnPreshow.Rotate(Vector3.forward * rotationOnZAxis);

        if (objectOnPreshow.GetComponent<SpriteRenderer>()) {
            var color = objectOnPreshow.GetComponent<SpriteRenderer>().color;
            var newColor = new Color(color.r, color.g, color.b, aplhaPreshow);
            objectOnPreshow.GetComponent<SpriteRenderer>().color = newColor;
        }
    }

    private void StopPreshowPrefab(Transform tileTransform, Vector2Int matrixPosition) {
        if (objectOnPreshow != null)
            Destroy(objectOnPreshow.gameObject);
    }
}
