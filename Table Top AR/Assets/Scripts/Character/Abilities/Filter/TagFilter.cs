using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.Character.Abilities.Filter
{
    [CreateAssetMenu(fileName = "FilterByTag", menuName = "ScriptableObjects /AbilityStrategies/Filters/FilterByTag")]
    public class TagFilter : FilterStrategy
    {
        [SerializeField]
        private string _tagToFilter = "";

        public override IEnumerable<GameObject> Filter(IEnumerable<GameObject> objectsToFilter)
        {
            foreach (var o in objectsToFilter)
            {
                if (o.CompareTag(_tagToFilter))
                {
                    yield return o;
                }
            }
        }
    }
}