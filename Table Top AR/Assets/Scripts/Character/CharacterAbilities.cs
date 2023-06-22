using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TableTopAR.Saving;
using UnityEngine;

namespace TableTopAR.Character.Abilities
{
    public class CharacterAbilities : MonoBehaviour, ISaveable
    {
        [SerializeField]
        private List<GenericAbility> _abilities = new List<GenericAbility>();

        public List<GenericAbility> Abilities { get => _abilities; }


        public object CaptureState()
        {
            var abilityRecords = _abilities.ToArray();
            return abilityRecords;
        }

        public void RestoreState(object state)
        {
            var abilitiesRecords = (GenericAbility[])state;
            _abilities = abilitiesRecords.ToList();
        }
    }
}