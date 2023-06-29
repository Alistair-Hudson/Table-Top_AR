using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TableTopAR.Character.Abilities.Filter
{
    [CreateAssetMenu(fileName = "SingleTarget", menuName = "ScriptableObjects /AbilityStrategies/Filters/SingleTarget")]
    public class SingleTarget : FilterStrategy
    {
        public override IEnumerable<GameObject> Filter(IEnumerable<GameObject> objectsToFilter)
        {
            foreach (var o in objectsToFilter)
            {
                if (!o.GetComponent<Health>().IsDead)
                {
                    yield return o;
                    break;
                }
            }
        }
    }
}