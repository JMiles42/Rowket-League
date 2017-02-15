using UnityEditor;
using UnityEngine;

public class LocalizationWindow : EditorWindow
{
    private static LocalizationMaster localizationMaster;
    private static bool ShowAvailableLanguages = true;
    private static bool EditAvailableLanguages = true;
    private static bool ShowLanguageEntries = true;
    private static string NewLangString = "";
    // Add menu named "My Window" to the Window menu
    [MenuItem("JMiles42/Localization/Language Editor")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        LocalizationWindow window = (LocalizationWindow)GetWindow (typeof (LocalizationWindow));
        localizationMaster = Resources.Load(LocalizationMaster.DefualtLocation) as LocalizationMaster;
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Language Editor", EditorStyles.boldLabel);
        if (localizationMaster)
        {
            GUILayout.Label("Localization Master Found In Resources Folder.", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            GUIContent saveContent = new GUIContent("Save active language", "Save active language");
            GUIContent LoadContent = new GUIContent("Load active language", "Load active language");
            if (GUILayout.Button(saveContent)) localizationMaster.Save();
            if (GUILayout.Button(LoadContent)) localizationMaster.Load();

            EditorGUILayout.EndHorizontal();

            ShowAvailableLanguages = EditorGUILayout.Toggle("Show Available Languages", ShowAvailableLanguages);
            if (ShowAvailableLanguages)
            {
                EditAvailableLanguages = EditorGUILayout.BeginToggleGroup("Show Available Languages",
                    EditAvailableLanguages);

                for (int i = 0; i < localizationMaster.AvailableLanguages.Count; i++)
                {
                    if (localizationMaster.AvailableLanguages[i] == LocalizationMaster.englishID)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Label(i.ToString(), EditorStyles.boldLabel, GUILayout.Width(20));
                        GUILayout.Label(localizationMaster.AvailableLanguages[i]);

                        GUIContent setContent = new GUIContent("Set English to active language", "Set _en to Active Language");
                        if (localizationMaster.LangIndex != i)
                        {
                            if (GUILayout.Button(setContent)) localizationMaster.SetLang(i);
                        }
                        else GUILayout.Label("Active", GUILayout.Width(100));
                    }
                    else
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Label(i.ToString(), EditorStyles.boldLabel, GUILayout.Width(20));
                        localizationMaster.AvailableLanguages[i] =
                            EditorGUILayout.TextArea(localizationMaster.AvailableLanguages[i]);

                        GUIContent setContent = new GUIContent("Set Active",string.Format("Set {0} to Active Language",localizationMaster.AvailableLanguages[i]));
                        if (localizationMaster.LangIndex != i)
                        {
                            if (GUILayout.Button(setContent, GUILayout.Width(100))) localizationMaster.SetLang(i);
                        }
                        else GUILayout.Label("Active", GUILayout.Width(100));

                        GUIContent resetContent = new GUIContent("Reset Language", "Reset Entries to Key only");
                        if (GUILayout.Button(resetContent, GUILayout.Width(100))) ResetLanguage(i);

                        GUIContent deleteContent = new GUIContent("X", "Delete Entry");
                        if (GUILayout.Button(deleteContent, GUILayout.Width(25)))
                            localizationMaster.AvailableLanguages.RemoveAt(i);
                    }
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.BeginHorizontal();
                NewLangString = EditorGUILayout.TextArea(NewLangString);
                if (GUILayout.Button("Add New Language")) NewLanguage();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndToggleGroup();
            }
            ShowLanguageEntries = EditorGUILayout.Toggle("Entries in the language Database. Show?", ShowLanguageEntries);
            if (ShowLanguageEntries)
            {
                for (int i = 0; i < localizationMaster.DictionaryData.Data.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label(i.ToString(), EditorStyles.boldLabel, GUILayout.Width(20));
                    localizationMaster.DictionaryData.Data[i] = new LocalizationDictionaryEntry
                    (
                        EditorGUILayout.TextArea(localizationMaster.DictionaryData.Data[i].Key, GUILayout.Width(position.width*0.2f)),
                        EditorGUILayout.TextArea(localizationMaster.DictionaryData.Data[i].Data)
                    );

                    GUIContent copyContent = new GUIContent("C", "Copy Entry");
                    if (GUILayout.Button(copyContent, GUILayout.Width(25)))
                        CopyPaste.Copy(localizationMaster.DictionaryData.Data[i]);
                    GUIContent pasteContent = new GUIContent("P", "Paste Entry");
                    if (GUILayout.Button(pasteContent, GUILayout.Width(25)))
                        localizationMaster.DictionaryData.Data[i] = CopyPaste.Paste<LocalizationDictionaryEntry>();

                    GUIContent deleteContent = new GUIContent("X", "Delete Entry");
                    if (GUILayout.Button(deleteContent, GUILayout.Width(25)))
                        localizationMaster.DictionaryData.Data.RemoveAt(i);
                    EditorGUILayout.EndHorizontal();
                }
                if (GUILayout.Button("Add New Entry")) localizationMaster.DictionaryData.Data.Add(new LocalizationDictionaryEntry("newKey", "newData"));
            }
        }
        else
        {
            GUILayout.Label("WARNING \n Localization Master NOT Found In Resources Folder.", EditorStyles.boldLabel);
            localizationMaster = Resources.Load(LocalizationMaster.DefualtLocation) as LocalizationMaster;
        }


        //groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        //myBool = EditorGUILayout.Toggle("Toggle", myBool);
        //myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        //EditorGUILayout.EndToggleGroup();
    }

    void NewLanguage()
    {
        localizationMaster.Save();
        localizationMaster.AvailableLanguages.Add(NewLangString);
        ResetLanguage(localizationMaster.AvailableLanguages.Count-1);
    }
    void ResetLanguage(int index)
    {
        int activeLag = localizationMaster.LangIndex;
        var engLagFile = localizationMaster.Load(0);
        var otherLagFile = engLagFile;
        for (int i = 0; i < otherLagFile.Data.Count; i++)
        {
            otherLagFile.Data[i] = new LocalizationDictionaryEntry(otherLagFile.Data[i].Key,"") ;
        }
        localizationMaster.SetLang(index);
        localizationMaster.DictionaryData = otherLagFile;
        localizationMaster.Save();
        localizationMaster.SetLang(activeLag);
    }
}
