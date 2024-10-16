using System;
using UnityEngine;

namespace UltraEvents.MonoBehaviours
{
    // Token: 0x0200000D RID: 13
    internal class BounceOffProjectiles : MonoBehaviour
    {
        // Token: 0x06000078 RID: 120 RVA: 0x00005E14 File Offset: 0x00004014
        [SerializeField] private float bounceRadius = 10f;
        [SerializeField] private float bounceForce = 50000f;
        [SerializeField] private float maxDistance = 1f;

        private void Update()
        {
            var hits = Physics.SphereCastAll(transform.position, bounceRadius, transform.forward, maxDistance);
            foreach (var hit in hits)
            {
                if (hit.rigidbody != null)
                {
                    if (hit.collider.TryGetComponent(out Projectile _) ||
                        hit.collider.TryGetComponent(out Magnet _) ||
                        hit.collider.TryGetComponent(out Grenade _))
                    {
                        hit.rigidbody.AddExplosionForce(bounceForce, transform.position, bounceRadius);
                    }
                }
            }
        }
    }
}
