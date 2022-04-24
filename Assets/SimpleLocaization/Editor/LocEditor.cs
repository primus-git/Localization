using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

namespace SimpleLocalization
{
    public class LocEditor : EditorWindow
    {
        public static LanguageController languageController;

        [MenuItem("Localization/MasterWindow")]
        public static void ShowWindow()
        {
            CheckForCurrentLocaliczation();
            EditorWindow window = GetWindow<LocEditor>("Simple Localization");

            window.minSize = new Vector2(300f, 300f);
        }

        private void OnGUI()
        {
            GetToolBar();


        }
       
        void GetToolBar()
        {
           
            //   Debug.LogError(toolInt);
         
                Getpanels();
                GetField();
            
           
        }
        string _name;
        void GetField()
        {
            GUILayout.Space(20);
            GUILayout.FlexibleSpace();
            _name = EditorGUILayout.TextField("Language Name", _name);
            if (GUILayout.Button("Create Language"))
            {
                if (!string.IsNullOrEmpty(_name))
                {
                    languageController.CreateNewlang(_name);
                }
                else
                {
                    Debug.LogError("Null Language name");
                    _name = null;
                }
                AssetDatabase.Refresh();
            }
            if (GUILayout.Button("Clear"))
            {
                if (EditorUtility.DisplayDialog("Please Confirm", "Are you sure you want to Delete language?", "Delete", "Cancel"))
                {
                    languageController.ClearList();
                }
                AssetDatabase.Refresh();
            }

            this.Repaint();
        }

        #region Left Panel
        void Getpanels()
        {
            if (languageController != null)
            {
                foreach (var s in languageController.languages)
                {
                    GUILayout.Space(5);
                    GUILayout.BeginHorizontal();

                    //   string temp = GUILayout.TextField((string)s.LanguageName);

                    GUILayout.Label(s.LanguageName.ToString());

                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Open"))
                    {
                        this.translation = s.translation; 
                        ShowOtherWindow();
                    }

                    GUILayout.EndHorizontal();
                }
            }
        }

        #endregion


        void OnEnable()
        {
            // Debug.Log("Editor Onenable");
            CheckForCurrentLocaliczation();
        }

        static void CheckForCurrentLocaliczation()
        {

            if (languageController != null)
                return;

            languageController = Resources.Load("LanguageController", typeof(LanguageController)) as LanguageController;
            if (languageController == null)
            {
                Debug.LogError("Insert");
                LanguageController asset = ScriptableObject.CreateInstance<LanguageController>();
                string path = "Assets/SimpleLocaization/Resources";
                CheckDirectry(path);
                path += "/LanguageController.asset";
                AssetDatabase.CreateAsset(asset, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = asset;
            }
        }

        public static void CheckDirectry(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public Translation translation;
        void ShowOtherWindow()
        {
            TranslationEditor.translation = this.translation;
            CheckForCurrentLocaliczation();
            string name = this.translation.name;
            EditorWindow window = GetWindow<TranslationEditor>(name);

            window.minSize = new Vector2(300f, 300f);
        }
    }
}
