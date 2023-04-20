using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using UnityEngine;

namespace TableTopAR.UI.DamageText
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField]
        private TMPro.TMP_Text displayText;

        public void SetText(string text)
        {
            displayText.text = text;
        }

        public void DestoryText()
        {
            Destroy(gameObject);
        }
    }
}