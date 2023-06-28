using System.Collections;
using System.Collections.Generic;
using TableTopAR.Character.Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace TableTopAR.UI.PlayerUI
{
    public class ActionSlotPassThrough : MonoBehaviour
    {
        [SerializeField]
        private Image _actionIcon = null;
        [SerializeField]
        private Image _coolDownOverlay = null;

        private CoolDownStore _coolDownStore = null;

        public Image ActionIcon { get => _actionIcon; }
        public Image CoolDownOverlay { get => _coolDownOverlay; }
        public GenericAbility Ability { get; set; }

        private void Start()
        {
            _coolDownStore = GetComponentInParent<CoolDownStore>();
        }

        private void Update()
        {
            _coolDownOverlay.fillAmount = _coolDownStore.CooldownFraction(Ability);
        }
    }
}