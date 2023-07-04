using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Core;
using UnityEngine;

namespace TableTopAR.Character.Abilities.Targeting
{
    [CreateAssetMenu(fileName = "LocationTarget", menuName = "ScriptableObjects /AbilityStrategies/Targeting/LocationTarget")]
    public class LocationTargeting : TargetingStrategy
    {
        [SerializeField]
        [PreviewField(75)]
        private Texture2D _cursorTexture;
        [SerializeField]
        [PreviewField(75)]
        private GameObject _targetingPrefab = null;
        [SerializeField]
        private Vector2 _cursorHotSpot;
        [SerializeField]
        private LayerMask _layerMask;
        [SerializeField]
        private float _areaEffectRadius;

        private Transform _targetingLocal = null;

        public override void StartTargeting(AbilityData data, Action finished)
        {
            if (_targetingLocal == null)
            {
                _targetingLocal = Instantiate(_targetingPrefab).transform;
                _targetingLocal.localScale = new Vector3(_areaEffectRadius, 1, _areaEffectRadius);
            }

            var player = data.User.GetComponent<GenericInput>();
            player.StartCoroutine(Targeting(data, player, finished));
        }

        private IEnumerator Targeting(AbilityData data, GenericInput player, Action finished)
        {
            var inputs = data.User.GetComponents<GenericInput>();
            foreach (var input in inputs)
            {
                input.enabled = false;
            }

            _targetingLocal.gameObject.SetActive(true);
            while (!data.IsCanceled)
            {
                Cursor.SetCursor(_cursorTexture, _cursorHotSpot, CursorMode.Auto);
#if UNITY_EDITOR
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#elif UNITY_ANDROID
            var player = user.GetComponent<PlayerInput>()
            Ray ray = player.ARCamera.ScreenPointToRay(player.RayCastPointTransform.position);
#endif
                if (Physics.Raycast(ray, out RaycastHit rayHit, float.PositiveInfinity, _layerMask))
                {
                    _targetingLocal.position = rayHit.point;
                    if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
                    {
                        //Absorbs whole input down
                        yield return new WaitWhile(() => Input.GetMouseButton(0) || Input.touchCount > 0);
                        data.TargetedPoint = rayHit.point;
                        data.Targets = GetObjectsInRadius(rayHit);
                        break;
                    }
                    yield return null;
                }
            }
            foreach (var input in inputs)
            {
                input.enabled = true;
            }
            _targetingLocal.gameObject.SetActive(false);
            finished();
        }

        private IEnumerable<GameObject> GetObjectsInRadius(RaycastHit rayHit)
        {
            var hits = Physics.SphereCastAll(rayHit.point, _areaEffectRadius, Vector3.up, 0);
            foreach (var hit in hits)
            {
                yield return hit.collider.gameObject;
            }
        }
    }
}