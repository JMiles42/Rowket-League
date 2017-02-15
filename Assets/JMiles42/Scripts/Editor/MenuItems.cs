using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuItems
{
    private const string mainFolder = "JMiles42/";
    [MenuItem(mainFolder + "Folders/Create Normal Folders")]
    static void CreateNormalFolders()
    {
        AssetDatabase.CreateFolder("Assets", "Scripts");
        AssetDatabase.CreateFolder("Assets", "Scenes");
        AssetDatabase.CreateFolder("Assets", "Art");
        AssetDatabase.CreateFolder("Art", "Models");
        AssetDatabase.CreateFolder("Art", "Textures");
        AssetDatabase.CreateFolder("Art", "Materials");
        AssetDatabase.CreateFolder("Assets", "Prefabs");
    }
    [MenuItem(mainFolder + "Folders/Create Resources Folders")]
    static void CreateResources()
    {
        AssetDatabase.CreateFolder( "Assets", "Resources" );
    }
    [MenuItem(mainFolder + "Folders/Create StreamingAssets Folders")]
    static void CreateStreamingAssets()
    {
        AssetDatabase.CreateFolder( "Assets", "StreamingAssets" );
    }
    [MenuItem(mainFolder + "Folders/Create Plugins Folders")]
    static void CreatePlugins()
    {
        AssetDatabase.CreateFolder( "Assets", "Plugins" );
    }
}
