using System;
using System.Collections;
using System.Collections.Generic;
using TableTopAR.Character;
using UnityEngine;

namespace TableTopAR.Items.Equipment.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private GameObject onHitEffect = null;
        [SerializeField]
        private GameObject[] destroyOnHit = null;
        [SerializeField]
        private float speed = 1;
        [SerializeField]
        private float maxLifeTime = 10;
        [SerializeField]
        private float lifeAfterImpact = 2;
        [SerializeField]
        private bool isHoming = false;

        public GameObject Instigator { get; set; }
        public Health Target { get; set; }
        public float Damage { get; set; }

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        void Update()
        {
            if (Target == null) return;
            if (isHoming && !Target.IsDead)
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if ((maxLifeTime -= Time.deltaTime) <= 0)
            {
                Destroy(gameObject);
            }
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
                if (hit.IsDead) return;
                if (onHitEffect != null)
                {
                    Instantiate(onHitEffect, transform.position, transform.rotation);
                }
                hit.TakeDamage(Instigator, Damage);

                foreach (var toDestory in destroyOnHit)
                {
                    Destroy(toDestory);
                }
                Destroy(gameObject, lifeAfterImpact);
                Target = null;
            }
        }
    }
}