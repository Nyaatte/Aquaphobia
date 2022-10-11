using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(SendMessage))]
public class SendMessageEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SendMessage sendMessageScript = (SendMessage)target;
        if(GUILayout.Button("SendMessage"))
        {
            sendMessageScript.SendCommand();
        }
    }
    
}

#endif
