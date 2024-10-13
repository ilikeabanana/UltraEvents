using UnityEngine;

namespace UltraEvents.MonoBehaviours
{
    // Token: 0x0200000F RID: 15
    internal class MoveAndTurn : MonoBehaviour
    {
        // Token: 0x0600007C RID: 124 RVA: 0x00005F90 File Offset: 0x00004190
        public void Awake()
        {
            Rigidbody rigidbody;
            bool flag = base.gameObject.TryGetComponent<Rigidbody>(out rigidbody);
            if (flag)
            {
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                bool flag2 = base.gameObject.GetComponentsInChildren<MeshCollider>() != null || base.gameObject.GetComponentsInChildren<MeshCollider>().Length != 0;
                if (flag2)
                {
                    MeshCollider[] componentsInChildren = base.gameObject.GetComponentsInChildren<MeshCollider>();
                    foreach (MeshCollider meshCollider in componentsInChildren)
                    {
                        meshCollider.convex = true;
                    }
                }
            }
            else
            {
                rigidbody = base.gameObject.AddComponent<Rigidbody>();
                rigidbody.constraints = rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
        }

        // Token: 0x0600007D RID: 125 RVA: 0x00006030 File Offset: 0x00004230
        public void FixedUpdate()
        {
            Vector3 forward = base.transform.forward;
            forward.y = 0f;
            base.transform.position += forward * this.moveSpeed;
        }

        // Token: 0x0600007E RID: 126 RVA: 0x0000607C File Offset: 0x0000427C
        private void OnCollisionEnter(Collision collision)
        {
            bool flag = this.collided;
            if (!flag)
            {
                this.collided = true;
                float num = Random.Range(-this.maxAngle, this.maxAngle);
                float z = base.transform.rotation.eulerAngles.z;
                float num2 = Random.Range(-this.maxAngle, this.maxAngle);
                Quaternion rotation = Quaternion.Euler(num2, num, z);
                base.transform.rotation = rotation;
            }
        }

        // Token: 0x0600007F RID: 127 RVA: 0x000060F5 File Offset: 0x000042F5
        private void OnCollisionExit(Collision collision)
        {
            this.collided = false;
        }

        // Token: 0x04000063 RID: 99
        private float moveSpeed = 0.5f;

        // Token: 0x04000064 RID: 100
        public float minAngle = 0f;

        // Token: 0x04000065 RID: 101
        public float maxAngle = 360f;

        // Token: 0x04000066 RID: 102
        private bool collided;
    }
}
