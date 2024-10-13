using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    // Token: 0x02000016 RID: 22
    public class AttachEverythingToPlayer : Effect
    {
        // Token: 0x060000A5 RID: 165 RVA: 0x00006C54 File Offset: 0x00004E54
        private void FixedUpdate()
        {
            Rigidbody[] array = Object.FindObjectsOfType<Rigidbody>();
            foreach (Rigidbody rigidbody in array)
            {
                bool flag = rigidbody == MonoSingleton<NewMovement>.Instance.rb;
                if (!flag)
                {
                    rigidbody.velocity = (MonoSingleton<NewMovement>.Instance.transform.position - rigidbody.transform.position).normalized * this.speed;
                }
            }
        }

        // Token: 0x04000077 RID: 119
        public float speed = 25f;
    }
}
