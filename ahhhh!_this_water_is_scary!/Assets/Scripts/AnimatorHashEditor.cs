using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(AnimatorHash))]
public class AnimatorHashEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AnimatorHash hashScript = (AnimatorHash)target;
        if (GUILayout.Button("Get Hash Value"))
        {
            hashScript.GetHash();
        }


    }
}

#endif
