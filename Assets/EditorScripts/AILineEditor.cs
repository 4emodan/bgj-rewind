using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AILine))]
public class AILineEditor : Editor
{
    private void OnSceneGUI()
    {
        AILine line = target as AILine;
        Transform handleTransform = line.transform;
        Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            handleTransform.rotation : Quaternion.identity;
        
        var p0_transform = handleTransform.TransformPoint(line.p0);
        var p1_transform = handleTransform.TransformPoint(line.p1);

        Handles.color = Color.white;

        Handles.DrawLine(p0_transform, p1_transform);
        EditorGUI.BeginChangeCheck();
        p0_transform = Handles.DoPositionHandle(p0_transform, handleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(line, "Move Point");
            EditorUtility.SetDirty(line);
            line.p0 = handleTransform.InverseTransformPoint(p0_transform);
            line.p0.y = 0;
        }

        EditorGUI.BeginChangeCheck();
        p1_transform = Handles.DoPositionHandle(p1_transform, handleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(line, "Move Point");
            EditorUtility.SetDirty(line);
            line.p1 = handleTransform.InverseTransformPoint(p1_transform);
            line.p1.y = 0;
        }
    }
}
