using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SocialPlatforms;
using NUnit.Framework;

namespace OneRoom
{
    public class TranslationsWindow : EditorWindow
    {
        [MenuItem("Window/Translations")]
        public static void Open()
        {
            GetWindow<TranslationsWindow>("Translations");
        }

        public Dictionary<string, string> translationMap;
        
        public string searchString = "";
        public Vector2 searchScroll;

        public string addKey = "";
        public string addValue = "";
        public Vector2 removeScroll;
        public string editKey = "";
        public string editValue = "";
        public string currentEditValue = "";
        public Vector2 editScroll;

        private bool showResults = false;
        private bool showAdd = false;
        private bool showRemove = false;
        private bool showEdit = false;

        private void OnEnable()
        {
            translationMap = LocalisationService.GetTranslationMap();
        }

        public void OnGUI()
        {
            GUILayout.Space(16);
            GUILayout.BeginHorizontal("box");

            if (GUILayout.Button("Add"))
            {
                showAdd = !showAdd;
                showRemove = false;
                showEdit = false;
            }

            if (GUILayout.Button("Remove"))
            {
                showAdd = false;
                showRemove = !showRemove;
                showEdit = false;
            }

            if (GUILayout.Button("Edit"))
            {
                showAdd = false;
                showRemove = false;
                showEdit = !showEdit;
            }

            GUILayout.EndHorizontal();

            ShowAdd();
            ShowRemove();
            ShowEdit();

            showResults = EditorGUILayout.Foldout(showResults, "Translations");
            if (showResults)
            {
                ShowResults();
            }
        }

        private void ShowResults()
        {
            if (!showResults)
            {
                return;
            }

            translationMap = LocalisationService.GetTranslationMap();

            GUILayout.BeginVertical("box");
            GUILayout.Space(8);
            searchString = EditorGUILayout.TextField("Search", searchString);
            GUILayout.Space(8);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Key", GUILayout.Width(128));
            GUILayout.Label("Value", GUILayout.Width(128));
            GUILayout.EndHorizontal();

            searchScroll = EditorGUILayout.BeginScrollView(searchScroll);

            foreach (KeyValuePair<string, string> map in translationMap)
            {
                string keyString = map.Key;
                string valueString = map.Value;

                if (IsSearchStringValid(keyString, valueString))
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.TextField(keyString, EditorStyles.textArea, GUILayout.Width(128));
                    GUILayout.TextField(valueString, EditorStyles.textArea, GUILayout.Width(128));
                    GUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.EndScrollView();
            GUILayout.EndVertical();
        }

        private void ShowAdd()
        {
            if (!showAdd)
            {
                return;
            }

            translationMap = LocalisationService.GetTranslationMap();

            EditorStyles.textArea.wordWrap = true;

            GUILayout.BeginHorizontal();
            GUILayout.Label("Key", GUILayout.Width(64));
            addKey = EditorGUILayout.TextArea(addKey, EditorStyles.textArea);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Value", GUILayout.Width(64));
            addValue = EditorGUILayout.TextArea(addValue, EditorStyles.textArea);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Confirm"))
            {
                if (addKey == string.Empty || addValue == string.Empty)
                {
                    ConfirmationDialog("Key/Value is Empty");
                    return;
                }

                if (CanUseAddKey(addKey))
                {
                    ConfirmationDialog("[" + addKey + "] already exists. Use a different one.");
                    return;
                }

                if (DecisionDialog("Adding Key Value Data"))
                {
                    LocalisationService.Add(addKey, addValue);
                }
            }
        }

        private void ShowRemove()
        {
            if (!showRemove)
            {
                return;
            }

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

            removeScroll = EditorGUILayout.BeginScrollView(removeScroll);

            EditorStyles.textArea.wordWrap = true;
            foreach (KeyValuePair<string, string> map in translationMap)
            {
                string keyString = map.Key;
                string valueString = map.Value;

                if (IsSearchStringValid(keyString, valueString))
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("x", GUILayout.Width(32)))
                    {
                        if (DecisionDialog("Removing Key Value Data"))
                        {
                            LocalisationService.Remove(keyString);
                        }
                    }
                    GUILayout.TextField(keyString, EditorStyles.textArea, GUILayout.Width(128));
                    GUILayout.TextField(valueString, EditorStyles.textArea, GUILayout.Width(128));
                    GUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.EndScrollView();
            GUILayout.EndVertical();
        }

        private void ShowEdit()
        {
            if (!showEdit)
            {
                return;
            }

            translationMap = LocalisationService.GetTranslationMap();

            EditorStyles.textArea.wordWrap = true;

            GUILayout.BeginHorizontal();
            GUILayout.Label("Key", GUILayout.Width(64));
            GUILayout.TextField(editKey, EditorStyles.textArea);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Value", GUILayout.Width(64));
            editValue = EditorGUILayout.TextArea(editValue, EditorStyles.textArea);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Confirm"))
            {
                if (editValue == string.Empty)
                {
                    ConfirmationDialog("Value is Empty");
                    return;
                }

                if (!HasEditValueChanged(editValue))
                {
                    ConfirmationDialog("Value is the same");
                    return;
                }

                if (DecisionDialog("Editing Key Value Data"))
                {
                    LocalisationService.Edit(editKey, editValue);
                }
            }

            GUILayout.BeginVertical("box");
            GUILayout.Space(8);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Search", GUILayout.Width(64));
            searchString = EditorGUILayout.TextField(searchString);
            GUILayout.EndHorizontal();
            GUILayout.Space(8);
            GUILayout.BeginHorizontal();
            GUILayout.Label("", GUILayout.Width(34));
            GUILayout.Label("Key", GUILayout.Width(128));
            GUILayout.Label("Value", GUILayout.Width(128));
            GUILayout.EndHorizontal();

            editScroll = EditorGUILayout.BeginScrollView(editScroll);

            foreach (KeyValuePair<string, string> map in translationMap)
            {
                string keyString = map.Key;
                string valueString = map.Value;

                if (IsSearchStringValid(keyString, valueString))
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("+", GUILayout.Width(32)))
                    {
                        editKey = keyString;
                        editValue = valueString;
                        currentEditValue = valueString;
                    }
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

        private bool HasEditValueChanged(string pValue)
        {
            bool valueChanged = !pValue.ToLower().Equals(currentEditValue.ToLower());

            return valueChanged;
        }

        private bool CanUseAddKey(string pKey)
        {
            bool canUseKey = false;

            foreach (KeyValuePair<string, string> map in translationMap)
            {
                string keyString = map.Key;

                if (pKey.ToLower().Equals(keyString.ToLower()))
                {
                    canUseKey = true;
                }
            }

            return canUseKey;
        }

        private bool DecisionDialog(string title)
        {
            string message = "Are you sure?";
            string ok = "Yes";
            string cancel = "No";

            return EditorUtility.DisplayDialog(title, message, ok, cancel);
        }

        private bool ConfirmationDialog(string message)
        {
            string title = "Warning";
            string ok = "Ok";

            return EditorUtility.DisplayDialog(title, message, ok);
        }
    }
}
