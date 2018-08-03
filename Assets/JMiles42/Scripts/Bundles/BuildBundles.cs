#if UNITY_EDITOR
using UnityEditor;

public class BuildBundles
{
	[MenuItem("Assets/Build Assest Bundles")]
	private static void BuildAssestsBundles()
	{
		BuildPipeline.BuildAssetBundles("Assests/Asset Bundles", BuildAssetBundleOptions.ForceRebuildAssetBundle, BuildTarget.StandaloneWindows64);
	}
}
#endif