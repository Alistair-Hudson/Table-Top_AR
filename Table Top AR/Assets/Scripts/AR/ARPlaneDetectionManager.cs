using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace TableTopAR.AR
{
    [RequireComponent(typeof(ARPlacementManager), typeof(ARPlaneManager))]
    public class ARPlaneDetectionManager : MonoBehaviour
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        [SerializeField]
        private PlayerInput playerInputControl;
        [SerializeField]
        private Button placeButton;
        [SerializeField]
        private GameObject scaleSlider;
        [SerializeField]
        private NavigationBaker _navigationBaker;
        [SerializeField]
        private GameObject _charaterPrefab;

        private ARPlaneManager planeManager;
        private ARPlacementManager placementManager;

        private void Awake()
        {
            playerInputControl.gameObject.SetActive(false);

            placementManager = GetComponent<ARPlacementManager>();
            planeManager = GetComponent<ARPlaneManager>();

            placeButton.onClick.AddListener(DisableARPlacement);
        }

        void Start()
        {
            placeButton.gameObject.SetActive(true);
            //adjustButton.SetActive(false);
            //searchForGameButton.SetActive(false);
            scaleSlider.SetActive(true);

            //infoPanel.text = "Move Phone to detect planes and place Arena";
        }

        public void DisableARPlacement()
        {
            SetAllPlanesState(false);
            planeManager.enabled = false;
            placementManager.enabled = false;

            placeButton.gameObject.SetActive(false);
            //adjustButton.SetActive(true);
            //searchForGameButton.SetActive(true);
            scaleSlider.SetActive(false);
            _navigationBaker.BakeNavMesh();
            playerInputControl.gameObject.SetActive(true);
            playerInputControl.Character = Instantiate(_charaterPrefab, Vector3.zero, Quaternion.identity).GetComponent<NavMeshAgent>();

            //infoPanel.text = "Search for to enter a battle or readjust arena";
        }

        private void SetAllPlanesState(bool state)
        {
            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(state);
            }
        }
#endif
    }
}
