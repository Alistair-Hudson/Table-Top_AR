using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Character;
using UnityEngine;

namespace TableTopAR.Core
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float speed = 1;

        public Health Target { get; set; }
        public float Damage { get; set; }

        void Update()
        {
            if (Target == null) return;
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private Vector3 GetAimLocation()
        {
            if (Target.TryGetComponent<CapsuleCollider>(out var targetCollider))
            {
                return Target.transform.position + Vector3.up * targetCollider.height / 2;
            }

            return Target.transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Health>(out var hit))
            {
                if (hit != Target) return;
                hit.TakeDamage(Damage);
                Destroy(gameObject);
            }
        }
    }
}