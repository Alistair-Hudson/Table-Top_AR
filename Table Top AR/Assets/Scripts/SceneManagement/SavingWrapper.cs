using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Saving;
using UnityEngine;

namespace TableTopAR.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        private const string defaultSaveFile = "save";

        private IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.ImediateFadeOut();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            yield return fader.FadeInOut(0);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
    }
}
