using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;
//using System.Collections.Generic;

namespace JMiles42.IO.Generic
{
	public static class SavingLoading
	{
		public static void LoadGameData<T>(string name, out T data)
		{
			//Does space magic if file exists
			if(File.Exists(Application.persistentDataPath + "/" + name + ".data")) //Does the file exist
			{
				//Space Magic
				var bf       = new BinaryFormatter();
				var filepath = Application.persistentDataPath + "/" + name + ".data";

				var file = File.Open(filepath, //Name file
				                     FileMode.Open);

				Debug.Log("Loaded : " + filepath);
				data = (T)bf.Deserialize(file);
				file.Close(); //End file
			}
			else
				data = default(T);
		}

		public static void SaveGameData<T>(string name, T data)
		{
			//Space Magic
			var bf       = new BinaryFormatter();
			var filepath = Application.persistentDataPath + "/" + name + ".data";

			var file = File.Open(filepath,             //Name file
			                     FileMode.OpenOrCreate //How to open file
			                    );

			Debug.Log("Saved : " + filepath);
			bf.Serialize(file, data); //Magic happens
			file.Close();             //End file
		}
	}
}