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

        int toolbarInt = 0;
        string[] toolbarStrings = { "Language", "Translation" };

        void GetToolBar()
        {
            toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarStrings);
            if (toolbarInt == 0)
            {
                Getpanels();
                GetField();
            }
            else
            {
                RenderLeftPanel();
            }
        }
        string _name;
        void GetField()
        {
            GUILayout.Space(20);
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
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);


                    string temp = GUILayout.TextField((string)s.LanguageName);

                    GUILayout.Label(temp);

                    if (GUILayout.Button("Open"))
                    {

                    }

                    GUILayout.EndHorizontal();
                }
            }
        }

        #endregion

        #region Right panel
        void RenderLeftPanel()
        {
            EditorGUILayout.BeginVertical();
            //source = EditorGUILayout.ObjectField(source, typeof(UnityEngine.Object), true);


            if (languageController != null)
            {

                foreach (var a in languageController.languages)
                {
                    foreach (var s in a.translation.GetType().GetFields())
                    {


                        GUILayout.BeginHorizontal();
                        GUILayout.Space(20);
                        GUILayout.Label(s.Name);

                        var val = s.GetValue(languageController);
                        Debug.Log(val);
                        if (val.GetType() == typeof(System.Int32))
                        {
                            // string temp = GUILayout.TextField(s.GetValue(itemData.configuration).ToString());
                            // int parsedValue = 0;
                            // int.TryParse(temp, out parsedValue);
                            // s.SetValue(itemData.configuration, parsedValue);
                        }

                        else if (val.GetType() == typeof(System.String))
                        {
                            string temp = GUILayout.TextField((string)s.GetValue(languageController));
                            s.SetValue(languageController, temp);
                        }

                        GUILayout.EndHorizontal();
                    }

                }
            }

            EditorGUILayout.EndVertical();
        }

        void RenderTranslationButtons()
        {

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



    }
}
