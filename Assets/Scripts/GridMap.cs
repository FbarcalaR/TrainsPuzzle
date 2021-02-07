using System;
using UnityEngine;

public class GridMap : MonoBehaviour {
    public Transform tilePrefab;
    public Sprite tileSprite;
    public Vector2Int mapSize;
    public Action<Transform, Vector2Int> tileClicked;
    public Action<Transform, Vector2Int> mouseEntersTile;
    public Action<Transform, Vector2Int> mouseExitsTile;

    [Range(0, 1)]
    public float outlinePercent;

    private readonly string holderName = "Generated Map";

    public void Start() {
        GenerateMap();
    }

    public void GenerateMap() {
        if (transform.Find(holderName)) {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        var spriteBounds = new Vector2(tileSprite.bounds.size.x, tileSprite.bounds.size.y);
        var offset = new Vector2(-mapSize.x * spriteBounds.x / 2 + spriteBounds.x / 2, -mapSize.y * spriteBounds.y / 2 + spriteBounds.y / 2);
        offset += (Vector2)transform.position;
        for (int x = 0; x < mapSize.x; x++) {
            for (int y = 0; y < mapSize.y; y++) {
                Vector3 tilePosition = new Vector3(offset.x + x * spriteBounds.x, offset.y + y * spriteBounds.y, 0);
                CreateNewTile(mapHolder, tilePosition, x, y);
            }
        }
    }

    private void CreateNewTile(Transform mapHolder, Vector3 tilePosition, int x, int y) {
        Transform newTileTransform = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
        newTileTransform.localScale = Vector3.one * (1 - outlinePercent);
        newTileTransform.parent = mapHolder;
        var tile = newTileTransform.gameObject.GetComponent<Tile>();

        tile.positionInMatrix.x = x;
        tile.positionInMatrix.y = y;

        tile.mouseClick += TileClicked;
        tile.mouseExit += MouseExitsTile;
        tile.mouseEnter += MouseEntersTile;
    }

    private void TileClicked(Transform tileTransform, Vector2Int matrixPosition) {
        tileClicked?.Invoke(tileTransform, matrixPosition);
    }

    private void MouseEntersTile(Transform tileTransform, Vector2Int matrixPosition) {
        mouseEntersTile?.Invoke(tileTransform, matrixPosition);
    }

    private void MouseExitsTile(Transform tileTransform, Vector2Int matrixPosition) {
        mouseExitsTile?.Invoke(tileTransform, matrixPosition);
    }
}
