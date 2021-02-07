using UnityEditor;

[CustomEditor(typeof(GridMap))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        GridMap map = target as GridMap;
        map.GenerateMap();
    }
}
