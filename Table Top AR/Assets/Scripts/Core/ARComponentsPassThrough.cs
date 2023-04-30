using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TableTopAR.Core
{
    public class ARComponentsPassThrough : MonoBehaviour
    {
        [SerializeField]
        private RawImage _rayCastPoint;
        public RawImage RayCastPoint { get => _rayCastPoint; }
        [SerializeField]
        private Camera _arCamera;
        public Camera ARCamera { get => _arCamera; }
    }
}