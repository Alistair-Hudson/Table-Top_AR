using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.AI
{
    public class PatrolPath : MonoBehaviour
    {
        const float _pointRadius = 0.3f;

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(transform.GetChild(i).position, _pointRadius);
                Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(GetNextIndex(i)).position);
            }
        }

        public int GetNextIndex(int i)
        {
            if (i + 1 >= transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        public Vector3 GetWayPoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
