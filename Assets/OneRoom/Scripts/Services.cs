using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

namespace OneRoom
{
    public class Services : MonoBehaviour
    {
        public static Services main = null;
        
        public void Init()
        {
            main = this;
        }

        public void SetLanguageCode(string pLanguageCode)
        {
            LocalisationService.SetLanguageCode(pLanguageCode);
        }

        public void LoadLocalisationService()
        {
            LocalisationService.Load();
        }

        public string GetTranslation(string pKey)
        {
            return LocalisationService.GetTranslation(pKey);
        }
    }
}
