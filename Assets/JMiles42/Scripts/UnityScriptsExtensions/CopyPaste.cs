using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class CopyPaste
{
    public static void Copy<T>(T value)
    {
        EditorGUIUtility.systemCopyBuffer = JsonUtility.ToJson(value);
    }
    public static T Paste<T>()
    {
        T value = JsonUtility.FromJson<T> (EditorGUIUtility.systemCopyBuffer);
        return value;
    }
    public static void Paste<T>(ref T obj)
    {
        JsonUtility.FromJsonOverwrite(EditorGUIUtility.systemCopyBuffer, obj);
    }
    public static void Paste<T>(T obj)
    {
        JsonUtility.FromJsonOverwrite(EditorGUIUtility.systemCopyBuffer, obj);
    }
    public static string Paste()
    {
        return EditorGUIUtility.systemCopyBuffer;
    }
}
