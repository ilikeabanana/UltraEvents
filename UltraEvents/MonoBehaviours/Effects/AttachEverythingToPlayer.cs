using System.Collections.Generic;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    // Token: 0x02000016 RID: 22
    public class AttachEverythingToPlayer : Effect
    {
        public float speed = 25f;
        public int maxAttachedObjects = 50; // New variable for maximum number of objects

        private List<Rigidbody> attachedObjects = new List<Rigidbody>(); // List to keep track of attached objects

        private void FixedUpdate()
        {
            Rigidbody[] allRigidbodies = Object.FindObjectsOfType<Rigidbody>();

            foreach (Rigidbody rigidbody in allRigidbodies)
            {
                if (rigidbody != MonoSingleton<NewMovement>.Instance.rb)
                {
                    if (!attachedObjects.Contains(rigidbody) && attachedObjects.Count < maxAttachedObjects)
                    {
                        attachedObjects.Add(rigidbody);
                    }

                    if (attachedObjects.Contains(rigidbody))
                    {
                        Vector3 direction = (MonoSingleton<NewMovement>.Instance.transform.position - rigidbody.transform.position).normalized;
                        rigidbody.velocity = direction * speed;
                    }
                }
            }

            // Remove any null references from the list (in case objects were destroyed)
            attachedObjects.RemoveAll(item => item == null);
        }
    }
}
