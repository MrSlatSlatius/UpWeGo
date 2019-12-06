using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scritps
{
    public class SubmarineMine : MonoBehaviour
    {
        private const float MAX_RADIUS = 5.0f;
        private const float MAX_EXPLOSION_FORCE_MODIFIER = 25.0f;

        [SerializeField]
        private Collider[] _nearbyObjects;

        private void OnTriggerEnter(Collider plyrCollider)
        {
            Explode(plyrCollider);
        }

        // Create explosion, adding explosion force to object within max range
        private void Explode(Collider otherCollider)
        {
            // Get nearby objects
            _nearbyObjects = Physics.OverlapSphere(transform.position, 
                MAX_RADIUS);
            foreach(Collider nearbyObj in _nearbyObjects)
            {
                PlayerMovement plyrMov = 
                    nearbyObj.GetComponent<PlayerMovement>();
                if(plyrMov != null)
                {
                    // Apply force
                    Vector3 dir = transform.position - 
                        plyrMov.transform.position;
                    Vector3 _explosionForce = dir.normalized * 
                        MAX_EXPLOSION_FORCE_MODIFIER;
                    plyrMov.AddForce(- _explosionForce);
                }
            }
            // Remove mine
            Destroy(gameObject);
        }
    }
}
