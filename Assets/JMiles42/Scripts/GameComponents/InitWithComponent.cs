using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitWithComponent<T> where T : Component
{
    public GameObject gameObject;
    public T ScriptComponent;

    public InitWithComponent(string name)
    {
        gameObject = new GameObject(name);
        ScriptComponent = gameObject.AddComponent<T>();
    }
    public static implicit operator T(InitWithComponent<T> fp)
    {
        return fp.ScriptComponent;
    }
    public static implicit operator GameObject(InitWithComponent<T> fp)
    {
        return fp.gameObject;
    }
}
