using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace OneRoom
{
    [CustomEditor(typeof(TextTranslator))]
    public class TextTranslatorEditor : Editor
    {
        public string searchString = "";
        public bool showResults = false;
        public Vector2 scroll;
        public Dictionary<string, string> translationMap;

        private TextTranslator textTranslator;

        private void OnEnable()
        {
            translationMap = LocalisationService.GetTranslationMap();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            textTranslator = (TextTranslator)target;

            showResults = EditorGUILayout.Foldout(showResults, "Translations");
            if (showResults)
            {
                ShowResults();
            }
        }

        private void ShowResults()
        {
            translationMap = LocalisationService.GetTranslationMap();

            GUILayout.BeginVertical("box");
            GUILayout.Space(8);
            searchString = EditorGUILayout.TextField("Search", searchString);
            GUILayout.Space(8);
            GUILayout.BeginHorizontal();
            GUILayout.Label("", GUILayout.Width(34));
            GUILayout.Label("Key", GUILayout.Width(128));
            GUILayout.Label("Value", GUILayout.Width(128));
            GUILayout.EndHorizontal();

            scroll = EditorGUILayout.BeginScrollView(scroll);

            foreach (KeyValuePair<string, string> map in translationMap)
            {
                string keyString = map.Key;
                string valueString = map.Value;

                if (IsSearchStringValid(keyString, valueString))
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("^", GUILayout.Width(32)))
                    {
                        textTranslator.key = keyString;
                    }
                    EditorStyles.textArea.wordWrap = true;
                    GUILayout.TextField(keyString, EditorStyles.textArea, GUILayout.Width(128));
                    GUILayout.TextField(valueString, EditorStyles.textArea, GUILayout.Width(128));
                    GUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.EndScrollView();
            GUILayout.EndVertical();
        }

        private bool IsSearchStringValid(string pKey, string pValue)
        {
            bool isKeyValid = pKey.ToLower().Contains(searchString.ToLower());
            bool isValueValid = pValue.ToLower().Contains(searchString.ToLower());

            return isKeyValid || isValueValid;
        }
    }
}
