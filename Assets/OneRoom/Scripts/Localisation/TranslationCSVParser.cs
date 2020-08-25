using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace OneRoom
{
    public class TranslationCSVParser
    {
        private string csvFilePath = "Assets/OneRoom/Scripts/Localisation/Resources/";
        private string csvFileName = "translations";
        private TextAsset csvTextAsset;
        private char lineSeparator = '\n';
        private char commaSeparator = ',';

        public void Load()
        {
            csvTextAsset = Resources.Load<TextAsset>(csvFileName);
        }

        private string[] GetStringLines()
        {
            string[] lines = csvTextAsset.text.Split(lineSeparator);

            if (lines[lines.Length - 1] == "")
            {
                lines[lines.Length - 1] = null;
            }

            return lines;
        }

        public List<string> GetLanguageCodes()
        {
            List<string> languageCodes = new List<string>();

            string[] headers = GetStringLines()[0].Split(commaSeparator);
            
            // start with 1 because the first one in the array is a title not language code
            // key, en_GB, ja_JP
            for (int i = 1; i < headers.Length; ++i)
            {
                string header = headers[i];
                languageCodes.Add(header);
            }

            return languageCodes;
        }

        public Dictionary<string, string> GetTranslationMap(string pLanguageCode)
        {
            Dictionary<string, string> outputValues = new Dictionary<string, string>();

            string[] lines = GetStringLines();

            int languageCodeIndex = GetLanguageCodes().IndexOf(pLanguageCode);
            
            // start with 1 because first line are language codes
            for (int i = 1; i < lines.Length; ++i)
            {
                if (lines[i] != null)
                {
                    string[] stringArray = lines[i].Split(commaSeparator);

                    string stringKey = stringArray[0];
                    string stringValue = stringArray[languageCodeIndex + 1];
                    outputValues.Add(stringKey, stringValue);
                }
            }

            return outputValues;
        }

#if UNITY_EDITOR
        public void Add(string pKey, string pValue)
        {
            string path = csvFilePath + csvFileName + ".csv";
            string appended = string.Format("\n{0},{1},", pKey, pValue);
            File.AppendAllText(path, appended);

            AssetDatabase.Refresh();
        }

        public void Remove(string pKey)
        {
            string[] lines = GetStringLines();

            int removeIndex = -1;
            for (int i = 1; i < lines.Length; ++i)
            {
                if (lines[i] != null)
                {
                    string[] stringArray = lines[i].Split(commaSeparator);
                    string _key = stringArray[0];
                    
                    if(_key.Contains(pKey))
                    {
                        removeIndex = i;
                        break;
                    }
                }
            }

            List<string> newLines = new List<string>();
            for (int i = 0; i < lines.Length; ++i)
            {
                if (lines[i] != null && i != removeIndex)
                {
                    newLines.Add(lines[i]);
                }
            }

            string path = csvFilePath + csvFileName + ".csv";
            string replaced = string.Join(lineSeparator.ToString(), newLines.ToArray());
            File.WriteAllText(path, replaced);

            AssetDatabase.Refresh();
        }

        public void Edit(string pKey, string pValue)
        {
            Remove(pKey);
            Add(pKey, pValue);
        }
#endif
    }
}
