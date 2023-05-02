using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace TableTopAR.AR
{
    [RequireComponent(typeof(ARPlacementManager), typeof(ARPlaneManager))]
    public class ARPlaneDetectionManager : MonoBehaviour
    {
        [SerializeField]
        private Button placeButton;
        [SerializeField]
        private GameObject scaleSlider;

        private ARPlaneManager planeManager;
        private ARPlacementManager placementManager;

        private static bool _isPlaying = false;
        public static bool IsPlaying { get => _isPlaying; }

        private void Awake()
        {
            placementManager = GetComponent<ARPlacementManager>();
            planeManager = GetComponent<ARPlaneManager>();

            placeButton.onClick.AddListener(DisableARPlacement);
        }

        void Start()
        {
            placeButton.gameObject.SetActive(true);
            scaleSlider.SetActive(true);
        }

        public void DisableARPlacement()
        {
            SetAllPlanesState(false);
            planeManager.enabled = false;

            placementManager.VirtualTable.SpawnLevel();
            placementManager.enabled = false;
            
            placeButton.gameObject.SetActive(false);
            scaleSlider.SetActive(false);

            _isPlaying = true;
        }

        private void SetAllPlanesState(bool state)
        {
            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(state);
            }
        }
    }
}
