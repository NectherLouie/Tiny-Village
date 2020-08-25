using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace OneRoom
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextTranslator : MonoBehaviour
    {
        public string key;
        private TMP_Text textField;

        private void Start()
        {
            textField = GetComponent<TMP_Text>();
            textField.text = LocalisationService.GetTranslation(key);
        }
    }
}
