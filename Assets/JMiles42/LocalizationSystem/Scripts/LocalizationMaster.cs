using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Localization Master", menuName = "Localization/Master", order = 5)]
public class LocalizationMaster: JMilesScriptableObject
{
	public const string englishID       = "_en";
	public const string DefaultLocation = "Localization Master";
	public const string fileID          = ".lang";
	public static string fullPath
	{
		get { return Application.streamingAssetsPath + "/Localization/"; }
	}
	public static string englishPath
	{
		get { return fullPath + englishID; }
	}
#if UNITY_EDITOR
	[SerializeField] private string langIndexStr = englishID;
	[SerializeField]
#endif
	private int langIndex;
	public int LangIndex
	{
		get { return langIndex; }
	}
	public List<string> AvailableLanguages = new List<string> {englishID};
	public string IndexLang
	{
		get { return AvailableLanguages[langIndex]; }
	}
	public LocalizationDictionary DictionaryData = new LocalizationDictionary(englishID);

	public string GetDataFromKey(string str)
	{
		return DictionaryData[str];
	}

	public string GetLangFilePath()
	{
		return fullPath + AvailableLanguages[langIndex] + fileID;
	}

	public string GetLangFilePath(string lang)
	{
		return fullPath + lang + fileID;
	}

	public void SetLang(string lang)
	{
		Save();

		if(AvailableLanguages.Contains(lang))
			langIndex = AvailableLanguages.IndexOf(lang);

		Load();
	}

	public void SetLang(int lang)
	{
		Save();
		langIndex = lang;
		Load();
	}

	[ContextMenu("Change Lang")]
	private void Change()
	{
		Save();
		langIndex = langIndex == 0? 1 : 0;
#if UNITY_EDITOR
		langIndexStr = IndexLang;
#endif
		Load();
	}

	public string GetJSONStringFromFile(string lang)
	{
		var filepath = GetLangFilePath(lang);

		return File.Exists(filepath)? File.ReadAllText(filepath) : "";
	}

#if UNITY_EDITOR
	private void OnEnable()
	{
		Load();
	}

	private void OnDisable()
	{
		Save();
	}

	[ContextMenu("Save")]
	public void Save()
	{
		var filepath = GetLangFilePath(IndexLang);

		if(!File.Exists(filepath))
			File.Create(filepath);

		File.WriteAllText(filepath, JsonUtility.ToJson(DictionaryData, true));
	}

	[ContextMenu("Load")]
	public void Load()
	{
		JsonUtility.FromJsonOverwrite(GetJSONStringFromFile(IndexLang), DictionaryData);
	}

	public LocalizationDictionary Load(int index)
	{
		var newData = new LocalizationDictionary(AvailableLanguages[index]);
		JsonUtility.FromJsonOverwrite(GetJSONStringFromFile(AvailableLanguages[index]), newData);

		return newData;
	}
#else
#endif
}

[Serializable]
public class LocalizationDictionary
{
	public string                            lang;
	public List<LocalizationDictionaryEntry> Data;
	public string this[string key]
	{
		get
		{
			for(var i = 0; i < Data.Count; i++)
				if(Data[i].Key == key)
					return Data[i].Data;

			return LocalizationMasterComponent.FilePath + ":" + key;
		}
	}

	public LocalizationDictionary(string lan)
	{
		Data = new List<LocalizationDictionaryEntry>();
		lang = lan;
	}

	public LocalizationDictionary(List<LocalizationDictionaryEntry> d, string lan)
	{
		Data = d;
		lang = lan;
	}
}

[Serializable]
public struct LocalizationDictionaryEntry
{
	public string Key;
	public string Data;

	public LocalizationDictionaryEntry(string k, string d)
	{
		Key  = k;
		Data = d;
	}
}