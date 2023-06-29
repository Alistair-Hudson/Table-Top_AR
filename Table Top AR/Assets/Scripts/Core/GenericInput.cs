using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Character;
using TableTopAR.Character.Abilities;
using TableTopAR.UI.PlayerUI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TableTopAR.Core
{
    [RequireComponent(typeof(Movement), typeof(Combat), typeof(Health))]
    public class GenericInput : MonoBehaviour
    {
        [System.Serializable]
        protected struct CursorMapping
        {
            public CursorType Type;
            public Texture2D Texture;
            public Vector2 Hotspot;
        }

        [SerializeField]
        private float maxNavProjection = 1f;
        [SerializeField]
        private CursorMapping[] cursorMappings = null;
        [SerializeField]
        private Button _abilityButtonPrefab = null;
        [SerializeField]
        protected Transform _abilityButtonBar = null;
        

        private Movement _movement;
        private Combat _combat;
        private Health _health;
        private Mana _mana;
        
        public Movement Movement { get => _movement; }
        public Combat Combat { get => _combat; }

        private CursorType _cachedCursorType = CursorType.None;


        protected virtual void Awake()
        {
            _movement = GetComponent<Movement>();
            _combat = GetComponent<Combat>();
            _health = GetComponent<Health>();
            _mana = GetComponent<Mana>();

            var abilities = GetComponent<CharacterAbilities>().Abilities;

            for (int i = 0; i < abilities.Count; i++)
            {
                int j = i;
                var abilityButton = Instantiate(_abilityButtonPrefab, _abilityButtonBar);
                var passThrough = abilityButton.GetComponent<ActionSlotPassThrough>();
                passThrough.ActionIcon.sprite = abilities[j].Icon;
                passThrough.Ability = abilities[j];
                if (abilities[j].IsPassive)
                {
                    abilityButton.interactable = false;
                    StartCoroutine(RunPassiveAbility(abilities[j]));
                }
                else
                {
                    abilityButton.onClick.AddListener(() =>
                    {
                        if (_health.IsDead)
                        {
                            return;
                        }
                        abilities[j].UseAbility(gameObject);
                    });
                }

            }
        }

        private IEnumerator RunPassiveAbility(GenericAbility genericAbility)
        {
            while (true)
            {
                if (!_health.IsDead)
                {
                    genericAbility.UseAbility(gameObject);
                }
                yield return null;
            }
        }

        protected void ProcessRaycast(Ray ray)
        {
            if (InteractWithUI(ray))
            {
                return;
            }
            if (_health.IsDead)
            {
                return;
            }
            if (InteractWithComponent(ray)) 
            {
                return; 
            }
            if (InteractWithMovement(ray))
            {
                return;
            }

            SetCursor(CursorType.None);
        }

        private void SetCursor(CursorType cursorType)
        {
            if (cursorType == _cachedCursorType)
            {
                return;
            }
            _cachedCursorType = cursorType;
            CursorMapping cursorMapping = GetCursor(cursorType);
            SetCursorImp(cursorMapping);
        }

        protected virtual void SetCursorImp(CursorMapping cursorMapping)
        {
            Cursor.SetCursor(cursorMapping.Texture, cursorMapping.Hotspot, CursorMode.Auto);
        }

        protected CursorMapping GetCursor(CursorType cursorType)
        {
            foreach (var map in cursorMappings)
            {
                if (map.Type == cursorType)
                {
                    return map;
                }
            }
            return cursorMappings[0];
        }

        private bool InteractWithUI(Ray ray)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }

        private bool InteractWithComponent(Ray ray)
        {
            RaycastHit[] hits = SortedRaycastAll(ray);
            foreach (var hit in hits)
            {
                var raycastables = hit.transform.GetComponents<IRayCastable>();
                foreach (var castable in raycastables)
                {
                    if (castable.HandleRaycast(this))
                    {
                        SetCursor(castable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        private RaycastHit[] SortedRaycastAll(Ray ray)
        {
            RaycastHit[] hits = Physics.RaycastAll(ray);
            float[] distances = new float[hits.Length];
            for(int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }

        private bool InteractWithMovement(Ray ray)
        {
            if (RayCastNavMesh(ray, out Vector3 destination))
            {
                if (!_movement.CanMoveTo(destination))
                {
                    return false;
                }
                if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
                {
                    _movement.SetDestination(destination);
                }
                SetCursor(CursorType.Move);
                return true;
            }
            return false;
        }

        private bool RayCastNavMesh(Ray ray, out Vector3 target)
        {
            target = Vector3.zero;
            if (!Physics.Raycast(ray, out RaycastHit rayHit))
            {
                return false;
            }
            
            if (!NavMesh.SamplePosition(rayHit.point, out var navHit, maxNavProjection, NavMesh.AllAreas))
            {
                return false;
            }

            target = navHit.position;
            return true;
        }
    }
}