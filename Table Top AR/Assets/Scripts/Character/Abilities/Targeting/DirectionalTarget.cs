using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using UnityEngine;

namespace TableTopAR.Character.Abilities.Targeting
{
    [CreateAssetMenu(fileName = "DirectionalTarget", menuName = "ScriptableObjects /AbilityStrategies/Targeting/DirectionalTarget")]
    public class DirectionalTarget : TargetingStrategy
    {
        [SerializeField]
        private LayerMask _layerMask;
        [SerializeField]
        private float _groundOffset = 1;

        public override void StartTargeting(AbilityData data, Action finished)
        {
#if UNITY_EDITOR
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#elif UNITY_ANDROID
            var player = user.GetComponent<PlayerInput>()
            Ray ray = player.ARCamera.ScreenPointToRay(player.RayCastPointTransform.position);
#endif
            if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, _layerMask))
            {
                data.TargetedPoint = hit.point + ray.direction * _groundOffset / ray.direction.y;
            }
            finished();
        }
    }
}