using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableTopAR.Core
{

    public class Persistents : MonoBehaviour
    {
        private void Awake()
        {
            if (FindObjectsOfType<Persistents>().Length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
