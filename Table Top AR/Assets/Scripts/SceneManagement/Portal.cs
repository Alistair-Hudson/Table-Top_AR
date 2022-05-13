using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace TableTopAR.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        public enum DestinationID
        {
            A, B, C, D, E, F, G
        }

        [SerializeField]
        private Transform _spawnPoint;
        public Transform SpawnPoint { get => _spawnPoint; private set => _spawnPoint = value; }
        [SerializeField]
        private int _sceneIndexToLoad = -1;
        [SerializeField]
        private DestinationID _destinationID;
        public DestinationID DestID { get => _destinationID; private set => _destinationID = value; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<GenericInput>(out GenericInput player))
            {
                StartCoroutine(SceneTransition());
            }
        }

        private IEnumerator SceneTransition()
        {
            DontDestroyOnLoad(gameObject);
            
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeInOut(1);

            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save();
            yield return SceneManager.LoadSceneAsync(_sceneIndexToLoad);
            wrapper.Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            wrapper.Save();

            yield return new WaitForSeconds(1);
            yield return fader.FadeInOut(0);
            
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GenericInput player = FindObjectOfType<GenericInput>();
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.SpawnPoint.position);
            player.transform.rotation = otherPortal.SpawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach(Portal p in FindObjectsOfType<Portal>())
            {
                if (p.DestID == _destinationID && p != this)
                {
                    return p;
                }
            }

            return null;
        }
    }
}
