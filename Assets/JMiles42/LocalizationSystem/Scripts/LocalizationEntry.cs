using UnityEngine;

[CreateAssetMenu(fileName = "Localization Entry", menuName = "Localization/Entry", order = 0)]
public class LocalizationEntry: ScriptableObject
{
	public string    ID;
	public AudioClip audioClip;
	public TextAsset textAsset;
}

[CreateAssetMenu(fileName = "Audio Clip Subtiles", menuName = "Localization/Audio Clip Subtiles", order = 1)]
public class AudioClipSubtiles: ScriptableObject
{
	public AudioClip audioClip;
	public TextAsset textAsset;
}