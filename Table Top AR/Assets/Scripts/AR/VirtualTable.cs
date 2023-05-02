using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.AR
{
    public class VirtualTable : MonoBehaviour
    {
        [SerializeField]
        private GameObject _tableTop = null;
        [SerializeField]
        private GameObject _levelPrefab = null;

        public void SpawnLevel()
        {
            Instantiate(_levelPrefab, transform);
            _tableTop.SetActive(false);
        }
    }
}