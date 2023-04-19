using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace TableTopAR.AR
{
    [RequireComponent(typeof(ARRaycastManager))]
    public class ARPlacementManager : MonoBehaviour
    {
        [SerializeField]
        private Camera arCamera;
        public Camera ARCamera { get => arCamera; }
        [SerializeField]
        private GameObject _terrain;
        [SerializeField]
        private RawImage _rayCastPointImage;
        public RawImage RayCastPointImage { get => _rayCastPointImage; }

        private ARRaycastManager raycastManager;
        private static List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

        private void Awake()
        {
            raycastManager = GetComponent<ARRaycastManager>();
        }

        void Update()
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);

            Ray ray = arCamera.ScreenPointToRay(screenCenter);

            if (raycastManager.Raycast(ray, raycastHits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = raycastHits[0].pose;

                Vector3 posPlacement = hitPose.position;
                _terrain.transform.position = posPlacement;
            }
        }
    }
}
