using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SimpleLocalization
{
    public class TranslationEditor : EditorWindow
    {
        static LanguageController languageController;
        public static Translation translation;
        private void OnGUI()
        {
            languageController = LocEditor.languageController;
            RenderLeftPanel();
            RenderTranslationButtons();
        }
        #region Right panel
        void RenderLeftPanel()
        {
            EditorGUILayout.BeginVertical();
            //source = EditorGUILayout.ObjectField(source, typeof(UnityEngine.Object), true);


            if (translation != null)
            {

                foreach (var s in translation.converts)
                {
              
                    GUILayout.Space(3);
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.TextField(s.Id.ToString(), s.Conversation);
                    GUILayout.EndHorizontal();

                }


                // s.SetValue(languageController.languages, temp);

                //  var val = s.GetValue(languageController);
                //
                //  if (val.GetType() == typeof(System.Int32))
                //  {
                //     int parsedValue = 0;
                //     int.TryParse(temp, out parsedValue);
                //  }
                //
                //  else if (val.GetType() == typeof(System.String))
                //  {
                //      string temp = GUILayout.TextField((string)s.GetValue(languageController));
                //      s.SetValue(languageController, temp);
                //  }
                //



            }

            EditorGUILayout.EndVertical();
        }

        void RenderTranslationButtons()
        {
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Update"))
            {
                AssetDatabase.Refresh();
            }

        }
        #endregion

    }
}
