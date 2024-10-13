using System;
using UnityEngine;

namespace UltraEvents.MonoBehaviours
{
    // Token: 0x0200000D RID: 13
    internal class BounceOffProjectiles : MonoBehaviour
    {
        // Token: 0x06000078 RID: 120 RVA: 0x00005E14 File Offset: 0x00004014
        private void Update()
        {
            RaycastHit[] array = Physics.SphereCastAll(base.transform.position, 10f, base.transform.forward, 1f);
            bool flag = array.Length != 0;
            if (flag)
            {
                foreach (RaycastHit raycastHit in array)
                {
                    bool flag2 = raycastHit.collider.gameObject.GetComponent<Projectile>() != null && raycastHit.rigidbody != null;
                    if (flag2)
                    {
                        raycastHit.rigidbody.AddExplosionForce(50000f, base.transform.position, 50f);
                    }
                    else
                    {
                        bool flag3 = raycastHit.collider.gameObject.GetComponent<Magnet>() != null && raycastHit.rigidbody != null;
                        if (flag3)
                        {
                            raycastHit.rigidbody.AddExplosionForce(50000f, base.transform.position, 50f);
                        }
                        else
                        {
                            bool flag4 = raycastHit.collider.gameObject.GetComponent<Grenade>() != null && raycastHit.rigidbody != null;
                            if (flag4)
                            {
                                raycastHit.rigidbody.AddExplosionForce(50000f, base.transform.position, 50f);
                            }
                        }
                    }
                }
            }
        }
    }
}
