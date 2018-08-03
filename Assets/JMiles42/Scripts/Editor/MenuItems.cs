using UnityEditor;

public class MenuItems
{
	private const string mainFolder = "JMiles42/";

	[MenuItem(mainFolder + "Folders/Create Normal Folders")]
	private static void CreateNormalFolders()
	{
		AssetDatabase.CreateFolder("Assets", "Scripts");
		AssetDatabase.CreateFolder("Assets", "Scenes");
		AssetDatabase.CreateFolder("Assets", "Art");
		AssetDatabase.CreateFolder("Art",    "Models");
		AssetDatabase.CreateFolder("Art",    "Textures");
		AssetDatabase.CreateFolder("Art",    "Materials");
		AssetDatabase.CreateFolder("Assets", "Prefabs");
	}

	[MenuItem(mainFolder + "Folders/Create Resources Folders")]
	private static void CreateResources()
	{
		AssetDatabase.CreateFolder("Assets", "Resources");
	}

	[MenuItem(mainFolder + "Folders/Create StreamingAssets Folders")]
	private static void CreateStreamingAssets()
	{
		AssetDatabase.CreateFolder("Assets", "StreamingAssets");
	}

	[MenuItem(mainFolder + "Folders/Create Plugins Folders")]
	private static void CreatePlugins()
	{
		AssetDatabase.CreateFolder("Assets", "Plugins");
	}
}