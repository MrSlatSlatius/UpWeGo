using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scritps
{
    public class SubmarineMine : MonoBehaviour
    {
        private const float MAX_RADIUS = 5.0f;
        private const float MAX_EXPLOSION_FORCE_MODIFIER = 100.0f;

        [SerializeField]
        private Collider _mineCollider;
        [SerializeField]
        Collider[] nearbyObjects;

        // private SpriteRenderer _mineRenderer;

        private void Awake()
        {
            _mineCollider = GetComponent<Collider>();
            // _mineRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {

        }

        private void OnTriggerEnter(Collider plyrCollider)
        {
            Explode(plyrCollider);
        }

        // Create explosion, adding explosion force to object within max range
        private void Explode(Collider otherCollider)
        {
            // Get nearby objects
            Physics.OverlapSphere(transform.position, MAX_RADIUS);
                // Add force
                // Damage

            // Remove mine
            Destroy(this);s
        }
    }
}
