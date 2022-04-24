using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace SimpleLocalization
{
    [ExecuteInEditMode]
    public class LanguageController : ScriptableObject
    {
        public static LanguageController instance;
        [System.Serializable]
        public class Language
        {
            public string LanguageName;
            public Translation translation;

            public Language(string name)
            {
#if UNITY_EDITOR
                LanguageName = name;
                string path = "Assets/SimpleLocaization/Resources/Translation";
                CheckDirectry(path);
                if (string.IsNullOrEmpty(LanguageName) || string.IsNullOrWhiteSpace(LanguageName))
                {
                    LanguageName = "Random_" + Random.Range(1000, 50000);

                }
                path = path + "/" + LanguageName + ".asset";

                Translation asset = ScriptableObject.CreateInstance<Translation>();
                if (!instance.CheckLanguage(asset.name))
                {
                    AssetDatabase.CreateAsset(asset, path);
                    AssetDatabase.SaveAssets();

                    AssetDatabase.Refresh();
                    EditorUtility.FocusProjectWindow();
                    Selection.activeObject = asset;
                    translation = asset;



                }
                else
                {
                    Debug.LogError("Language already exists");
                }
#endif
            }
        }
        private void OnEnable()
        {
            GetReferenece();
        }
        void GetReferenece()
        {
            instance = null;
            instance = this;
        }
        public static void CheckDirectry(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        public List<Language> languages = new List<Language>();

        public void CreateNewlang(string name)
        {
            GetReferenece();
            Language language = new Language(name);

            if (!CheckLanguage(name))
            {
                languages.Add(language);
            }
        }


        public bool CheckLanguage(string AssetName)
        {
            foreach (var s in languages)
            {
                if (s.translation.name == AssetName)
                {
                    return true;
                }
            }
            return false;
        }

        public void ClearList()
        {
#if UNITY_EDITOR
            foreach (var s in languages)
            {
                File.Delete(AssetDatabase.GetAssetPath(s.translation));
            }
#endif

            languages.Clear();


        }
    }
}
