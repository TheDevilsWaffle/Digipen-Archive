///////////////////////////////////////////////////////////////////////////////////////////////////
//SCRIPT — EditorCamera.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class EditorCamera : MonoBehaviour
{
#if UNITY_EDITOR

    EditorCamera()
    {
        EditorApplication.update += EditorUpdate;
    }

    void Start()
    {
        if (EditorApplication.isPlaying)
            Destroy(gameObject);
    }

    void EditorUpdate()
    {
        if (this == null) return;
        if (Camera.current == null || !enabled) return;
        Transform _camera = Camera.current.transform;

        transform.position = _camera.position;
        transform.rotation = _camera.rotation;
    }

    ~EditorCamera()
    {
        EditorApplication.update -= EditorUpdate;
    }

#endif
}
