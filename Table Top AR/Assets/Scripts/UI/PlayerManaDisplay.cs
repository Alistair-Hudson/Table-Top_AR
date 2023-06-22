using System.Collections;
using System.Collections.Generic;
using TableTopAR.Character;
using UnityEngine;
using UnityEngine.UI;

namespace TableTopAR.UI.PlayerUI
{
    public class PlayerManaDisplay : MonoBehaviour
    {
        [SerializeField]
        private Image _manaDisplay = null;
        [SerializeField]
        private TMPro.TMP_Text _manaText = null;

        private Mana _mana = null;

        private void Start()
        {
            _mana = GetComponentInParent<Mana>();
            _mana.OnManaChange.AddListener(UpdateManaDisplay);
        }

        private void UpdateManaDisplay()
        {
            _manaDisplay.fillAmount = _mana.CurrentMana / _mana.MaxMana;
            _manaText.text = $"{_mana.CurrentMana.ToString("0")}/{_mana.MaxMana.ToString("0")}";
        }
    }
}