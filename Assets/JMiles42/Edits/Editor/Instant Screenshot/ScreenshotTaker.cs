//C# Example

using System;
using System.IO;
using UnityEditor;
using UnityEngine;
//Originaly created by Saad Khawaja
//https://www.assetstore.unity3d.com/en/#!/publisher/5951");
//Altered by Jordan Miles for his own useability

[ExecuteInEditMode]
public class Screenshot: EditorWindow
{
	private static string path          = Application.persistentDataPath + @"/Screenshots";
	private        int    HighResWidth  = 1920;
	private        int    HighResHeight = 1080;
	private        int    scale         = 2;
	public         Camera myCamera;
	//bool showPreview = true;
	private RenderTexture renderTexture;
	private bool          isTransparent;
	private float         lastTime;
	private bool          takeHiResShot;
	public  string        lastScreenshot = "";

	// Add menu item named "My Window" to the Window menu
	[MenuItem("Tools/Screenshot")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		var editorWindow = GetWindow(typeof(Screenshot));
		editorWindow.autoRepaintOnSceneChange = true;
		editorWindow.Show();
		editorWindow.titleContent = new GUIContent("Screenshot");
		Directory.CreateDirectory(Application.persistentDataPath + @"/Screenshots");
	}

	private void OnGUI()
	{
		EditorGUILayout.LabelField("Resolution", EditorStyles.boldLabel);
		HighResWidth  = EditorGUILayout.IntField("Width",  HighResWidth);
		HighResHeight = EditorGUILayout.IntField("Height", HighResHeight);
		EditorGUILayout.Space();
		scale = EditorGUILayout.IntSlider("Scale", scale, 1, 16);
		EditorGUILayout.Space();
		GUILayout.Label("Save Path", EditorStyles.boldLabel);
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.TextField(path, GUILayout.ExpandWidth(true));

		if(GUILayout.Button("Browse", GUILayout.ExpandWidth(false)))
			path = EditorUtility.SaveFolderPanel("Path to Save Images", path, Application.dataPath);

		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();

		//isTransparent = EditorGUILayout.Toggle(isTransparent,"Transparent Background");
		GUILayout.Label("Select Camera", EditorStyles.boldLabel);
		myCamera = EditorGUILayout.ObjectField(myCamera, typeof(Camera), true, null) as Camera;

		if(myCamera == null)
			myCamera = Camera.main;

		isTransparent = EditorGUILayout.Toggle("Transparent Background", isTransparent);
		EditorGUILayout.Space();
		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField("Default Options", EditorStyles.boldLabel);

		if(GUILayout.Button("Set To Screen Size"))
		{
			HighResHeight = (int)Handles.GetMainGameViewSize().y;
			HighResWidth  = (int)Handles.GetMainGameViewSize().x;
		}

		if(GUILayout.Button("Default Size"))
		{
			HighResHeight = 1080;
			HighResWidth  = 1920;
			scale         = 2;
		}

		EditorGUILayout.EndVertical();
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Screenshot will be taken at " + (HighResWidth * scale) + " x " + (HighResHeight * scale) + " px", EditorStyles.boldLabel);

		if(GUILayout.Button("Take Screenshot", GUILayout.MinHeight(60)))
		{
			if(path == "")
			{
				path = EditorUtility.SaveFolderPanel("Path to Save Images", path, Application.persistentDataPath);
				Debug.Log("Path Set");
				TakeHiResShot();
			}
			else
				TakeHiResShot();
		}

		EditorGUILayout.Space();
		EditorGUILayout.BeginHorizontal();

		if(GUILayout.Button("Open Last Screenshot", GUILayout.MaxWidth(160), GUILayout.MinHeight(40)))
		{
			if(lastScreenshot != "")
			{
				Application.OpenURL("file://" + lastScreenshot);
				Debug.Log("Opening File "     + lastScreenshot);
			}
		}

		if(GUILayout.Button("Open Folder", GUILayout.MaxWidth(100), GUILayout.MinHeight(40)))
			Application.OpenURL("file://" + path);

		//		if(GUILayout.Button("More Assets",GUILayout.MaxWidth(100),GUILayout.MinHeight(40)))
		//		{
		//			Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/publisher/5951");
		//		}
		EditorGUILayout.EndHorizontal();

		if(takeHiResShot)
		{
			var resWidthN  = HighResWidth;
			var resHeightN = HighResHeight;
			var rt         = new RenderTexture(resWidthN, resHeightN, 24);
			myCamera.targetTexture = rt;
			TextureFormat tFormat;

			if(isTransparent)
				tFormat = TextureFormat.ARGB32;
			else
				tFormat = TextureFormat.RGB24;

			var screenShot = new Texture2D(resWidthN, resHeightN, tFormat, false);
			myCamera.Render();
			RenderTexture.active = rt;
			screenShot.ReadPixels(new Rect(0, 0, resWidthN, resHeightN), 0, 0);
			myCamera.targetTexture = null;
			RenderTexture.active   = null;
			var bytes    = screenShot.EncodeToPNG();
			var filename = ScreenShotName(resWidthN, resHeightN);
			File.WriteAllBytes(filename, bytes);
			Debug.Log(string.Format("Took screenshot to: {0}", filename));
			//Application.OpenURL(filename);
			takeHiResShot          = false;
			resWidthN              = HighResWidth  * scale;
			resHeightN             = HighResHeight * scale;
			rt                     = new RenderTexture(resWidthN, resHeightN, 24);
			myCamera.targetTexture = rt;
			screenShot             = new Texture2D(resWidthN, resHeightN, tFormat, false);
			myCamera.Render();
			RenderTexture.active = rt;
			screenShot.ReadPixels(new Rect(0, 0, resWidthN, resHeightN), 0, 0);
			myCamera.targetTexture = null;
			RenderTexture.active   = null;
			bytes                  = screenShot.EncodeToPNG();
			filename               = ScreenShotName(resWidthN, resHeightN);
			File.WriteAllBytes(filename, bytes);
			Debug.Log(string.Format("Took screenshot to: {0}", filename));
			//Application.OpenURL(filename);
			takeHiResShot = false;
		}
	}

	public string ScreenShotName(int width, int height)
	{
		var strPath = "";
		strPath        = string.Format("{0}/screenshot_{3}_Dim[{1}x{2}].png", path, width, height, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
		lastScreenshot = strPath;

		return strPath;
	}

	public void TakeHiResShot()
	{
		Debug.Log("Taking Screenshot");
		takeHiResShot = true;
	}
}

[Serializable]
public class ScreenShotSettings
{
	private readonly int    resWidth;
	private readonly int    resHeight;
	public           Camera myCamera;
	private readonly int    scale;

	public ScreenShotSettings()
	{
		resWidth  = 1920;
		resHeight = 1080;
		scale     = 2;
		myCamera  = Camera.main;
	}

	public ScreenShotSettings(int _resWidth, int _resHeight, int _scale, Camera _cam)
	{
		resWidth  = _resWidth;
		resHeight = _resHeight;
		scale     = _scale;
		myCamera  = _cam;
	}

	public int GetWidth(bool withScale = false)
	{
		if(withScale)
			return resWidth * scale;

		return resWidth;
	}

	public int GetHeight(bool withScale = false)
	{
		if(withScale)
			return resHeight * scale;

		return resHeight;
	}
}