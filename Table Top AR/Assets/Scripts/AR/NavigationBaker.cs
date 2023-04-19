using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

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
