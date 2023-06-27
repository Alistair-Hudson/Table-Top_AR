using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.Character.Abilities
{
    public abstract class FilterStrategy : ScriptableObject
    {
        public abstract IEnumerable<GameObject> Filter(IEnumerable<GameObject> objectsToFilter);

    }
}