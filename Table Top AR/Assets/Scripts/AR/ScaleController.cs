using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;

namespace TableTopAR.AR
{
    [RequireComponent(typeof(XROrigin))]
    public class ScaleController : MonoBehaviour
    {
        [SerializeField]
        private Slider _scaleSlider = null;

        private XROrigin _xrOrigin = null;

        private void Awake()
        {
            _xrOrigin = GetComponent<XROrigin>();
        }

        private void Start()
        {
            _scaleSlider.onValueChanged.AddListener(OnSliderValueChange);
        }

        public void OnSliderValueChange(float value)
        {
            _xrOrigin.transform.localScale = Vector3.one * value;
        }
    }
}