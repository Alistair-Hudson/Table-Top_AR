using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TableTopAR.AR
{
    public class NavigationBaker : MonoBehaviour
    {
        [SerializeField]
        private NavMeshSurface _terrian;

        public void BakeNavMesh()
        {
            _terrian.BuildNavMesh();
        }
    }
}
