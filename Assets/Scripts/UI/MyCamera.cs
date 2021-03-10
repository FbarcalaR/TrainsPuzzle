using UnityEngine;
     
[ExecuteInEditMode]
public class MyCamera : MonoBehaviour {
    private Camera m_camera;

    [SerializeField]
    private float m_orthographicSize = 5f;

    private void Start() {
        RefreshCamera();
    }

    public void RefreshCamera() {
        if (m_camera == null)
            m_camera = GetComponent<Camera>();

        AdjustCamera(m_camera.aspect);
    }

    private void AdjustCamera(float aspect) {
        float _1OverAspect = 1f / aspect;
        m_camera.orthographicSize = m_orthographicSize * _1OverAspect;
    }

#if UNITY_EDITOR
    private void OnValidate() {
        RefreshCamera();
    }
#endif
}
