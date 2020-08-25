using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneRoom
{
    public class LocalisationService
    {
        private static string languageCode = "en_GB";
        private static TranslationCSVParser translationCSVParser;
        private static bool hasLoaded = false;

        public static void SetLanguageCode(string pLanguageCode)
        {
            languageCode = pLanguageCode;
        }

        public static void Load()
        {
            translationCSVParser = new TranslationCSVParser();
            translationCSVParser.Load();

            hasLoaded = true;
        }

        public static string GetTranslation(string pKey)
        {
            string output = null;

            if (!hasLoaded)
            {
                Load();
            }

            Dictionary<string, string> translationMap = translationCSVParser.GetTranslationMap(languageCode);

            if (translationMap.ContainsKey(pKey))
            {
                output = translationMap[pKey];
            }
            
            return output;
        }

        public static Dictionary<string, string> GetTranslationMap()
        {
            if (!hasLoaded)
            {
                Load();
            }

            return translationCSVParser.GetTranslationMap(languageCode);
        }

        public static void Add(string pKey, string pValue)
        {
            if (!hasLoaded)
            {
                Load();
            }

            translationCSVParser.Add(pKey, pValue);
        }

        public static void Remove(string pKey)
        {
            if (!hasLoaded)
            {
                Load();
            }

            translationCSVParser.Remove(pKey);
        }

        public static void Edit(string pKey, string pValue)
        {
            if (!hasLoaded)
            {
                Load();
            }

            translationCSVParser.Edit(pKey, pValue);
        }
    }
}
