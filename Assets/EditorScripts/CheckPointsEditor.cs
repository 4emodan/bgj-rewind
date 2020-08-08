using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CheckPoints))]
public class CheckPointsEditor : Editor
{
    private void OnSceneGUI()
    {
        CheckPoints checkPoints = target as CheckPoints;
        Transform handleTransform = checkPoints.transform;
        Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            handleTransform.rotation : Quaternion.identity;
        Vector3[] path_transform = new Vector3[checkPoints.points.Length];

        for (int i = 0; i < checkPoints.points.Length; i++)
        {
            path_transform[i] = handleTransform.TransformPoint(checkPoints.points[i]);
        }

        for (int i = 0; i < path_transform.Length; i++)
        {
            EditorGUI.BeginChangeCheck();
            path_transform[i] = Handles.DoPositionHandle(path_transform[i], handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(checkPoints, "Move Point");
                EditorUtility.SetDirty(checkPoints);
                checkPoints.points[i] = handleTransform.InverseTransformPoint(path_transform[i]);
            }
        }
    }
}