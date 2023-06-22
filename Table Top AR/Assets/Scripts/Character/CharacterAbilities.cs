using System.Collections;
using System.Collections.Generic;
using TableTopAR.Saving;
using UnityEngine;

namespace TableTopAR.Character.Abilities
{
    public class CharacterAbilities : MonoBehaviour, ISaveable
    {
        [SerializeField]
        private List<Ability> _abilities = new List<Ability>();

        public List<Ability> Abilities { get => _abilities; }


        public object CaptureState()
        {
            throw new System.NotImplementedException();
        }

        public void RestoreState(object state)
        {
            throw new System.NotImplementedException();
        }
    }
}