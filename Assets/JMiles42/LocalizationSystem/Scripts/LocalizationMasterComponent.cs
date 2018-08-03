using System.Collections.Generic;
using JMiles42.Data;
using UnityEngine;

public class LocalizationMasterComponent: SingletonCrate<LocalizationMasterComponent>
{
	public LangFile data;
	public static string FilePath
	{
		get { return Application.streamingAssetsPath + "/Localization/"; }
	}

	private void Awake()
	{
		InitObject();
	}

	public override void InitObject()
	{
		data.BuildData(Resources.Load("Localization Master") as LocalizationMaster);
	}

	public string GetDataFromKey(string key)
	{
		if(data.LanguageData.ContainsKey(key))
			return data.LanguageData[key];

		return FilePath + ":" + key;
	}
}

public class LangFile
{
	public Dictionary<string, string> LanguageData = new Dictionary<string, string>();

	public void BuildData(LocalizationMaster data)
	{
		LanguageData.Clear();

		foreach(var VARIABLE in data.DictionaryData.Data)
			LanguageData.Add(VARIABLE.Key, VARIABLE.Data);
	}
}