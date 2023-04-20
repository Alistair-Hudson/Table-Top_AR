using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using UnityEngine;

namespace TableTopAR.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [System.Serializable]
        public struct DamageTypeData
        {
            public DamageType Type;
            public Color Color;
        }

        [SerializeField]
        private DamageText damageTextPrefab;
        [SerializeField]
        private List<DamageTypeData> damageTypeDatas = new List<DamageTypeData>();

        private Dictionary<DamageType, Color> damageColorDict = new Dictionary<DamageType, Color>();

        private void Awake()
        {
            foreach (var data in damageTypeDatas)
            {
                damageColorDict.Add(data.Type, data.Color);
            }
        }

        public void Spawn(float damageAmount, DamageType damageType)
        {
            var dt = Instantiate(damageTextPrefab, transform);
            dt.SetText(damageAmount.ToString());
        }
    }
}