using UnityEngine;
using System;
using System.Linq;
using JetBrains.Annotations;

//		Returns the instance of this singleton.
//		Modifided Code originaly tip number 29 from:
//		http://devmag.org.za/2012/07/12/50-tips-for-working-with-unity-best-practices/
//		I don't understand all the logic behind it but it only allows one script of this type to exist
//		Making talking between scripts simple
namespace JMiles42.Data
{
	[Serializable]
	[DisallowMultipleComponent]
    public class Singleton<S> : JMilesBehaviour where S : JMilesBehaviour
    {
        protected static S instance;
        public static S Instance
        {
            get
            {
                if (instance) return instance;
                instance = (S) FindObjectOfType(typeof(S));
                if (!instance)
                    Debug.LogError(typeof(S) + " is needed in the scene.");//Print error
                return instance;
            }
        }
    }
    public abstract class SingletonCrate<S> : JMilesBehaviour where S : JMilesBehaviour
    {
        protected static S instance;
        public static S Instance
        {
            get
            {
                if (instance) return instance;
                instance = (S) Resources.FindObjectsOfTypeAll(typeof(S)).FirstOrDefault();
                if (!instance) CreateGameObject();
                return instance;
            }
        }

        private static void CreateGameObject()
        {
            GameObject gameObj = new GameObject();
            instance = gameObj.AddComponent<S>();
        }
        public abstract void InitObject();
    }
    public class SingletonScriptableObject<S> : ScriptableObject where S : ScriptableObject
    {
        protected static S instance;
        public static S Instance
        {
            get
            {
                if (instance) return instance;
                instance = (S) Resources.FindObjectsOfTypeAll<S>().FirstOrDefault();
                if (!instance)
                    Debug.LogError(typeof(S) + " is needed in the scene.");//Print error
                return instance;
            }
        }
    }
    public class SingletonBaseScriptableObject<S> : JMilesScriptableObject where S : JMilesScriptableObject
    {
        protected static S instance;
        public static S Instance
        {
            get
            {
                if (instance) return instance;
                instance = (S) Resources.FindObjectsOfTypeAll<S>().FirstOrDefault();
                if (!instance)
                    Debug.LogError(typeof(S) + " is needed");//Print error
                return instance;
            }
        }
    }
    public class Singlecalss : Singleton<Singlecalss>
	{

	}
}
