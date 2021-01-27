//-------------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2019 Tasharen Entertainment Inc
//-------------------------------------------------

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TweenRotation))]
public class TweenRotationEditor : UITweenerEditor
{
    public override void OnInspectorGUI()
    {
        GUILayout.Space(6f);
        NGUIEditorTools.SetLabelWidth(120f);

        TweenRotation tw = target as TweenRotation;
        GUI.changed = false;

        Vector3 from = EditorGUILayout.Vector3Field("From", tw.from);
        Vector3 to = EditorGUILayout.Vector3Field("To", tw.to);
        var quat = EditorGUILayout.Toggle("Quaternion", tw.quaternionLerp);

        if (GUI.changed)
        {
            NGUIEditorTools.RegisterUndo("Tween Change", tw);
            tw.from = from;
            tw.to = to;
            tw.quaternionLerp = quat;
            NGUITools.SetDirty(tw);
        }

        DrawCommonProperties();
    }
}