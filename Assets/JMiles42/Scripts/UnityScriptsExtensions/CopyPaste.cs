using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class CopyPaste
{
    public static void Copy<T>(T value)
    {
        EditorGUIUtility.systemCopyBuffer = EditorJsonUtility.ToJson(value);
    }
    public static T Paste<T>()
    {
        T value = JsonUtility.FromJson<T> (Paste());
        return value;
    }
    public static void Paste<T>(ref T obj)
    {
        EditorJsonUtility.FromJsonOverwrite(Paste(), obj);
    }
    public static void Paste<T>(T obj)
    {
        EditorJsonUtility.FromJsonOverwrite(Paste(), obj);
    }
    public static string Paste()
    {
        return EditorGUIUtility.systemCopyBuffer;
    }
}
