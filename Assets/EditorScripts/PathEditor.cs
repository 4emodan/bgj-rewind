using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Path))]
public class PathEditor : Editor
{
    private void OnSceneGUI()
    {
        Path path = target as Path;
        Transform handleTransform = path.transform;
        Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            handleTransform.rotation : Quaternion.identity;
        Vector3[] path_transform = new Vector3[path.points.Length];

        Vector3 start_transform = handleTransform.TransformPoint(path.start);
        for (int i = 0; i < path.points.Length; i++)
        {
            path_transform[i] = handleTransform.TransformPoint(path.points[i]);
        }

        Handles.color = Color.white;
        for (int i = -1; i < path_transform.Length - 1; i++)
        {
            if (i == -1)
            {
                Handles.DrawLine(start_transform, path_transform[0]);
            }
            else
            {
                Handles.DrawLine(path_transform[i], path_transform[i + 1]);
            }
        }

        for (int i = 0; i < path_transform.Length; i++)
        {
            EditorGUI.BeginChangeCheck();
            path_transform[i] = Handles.DoPositionHandle(path_transform[i], handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(path, "Move Point");
                EditorUtility.SetDirty(path);
                path.points[i] = handleTransform.InverseTransformPoint(path_transform[i]);
            }
        }

    }
}
