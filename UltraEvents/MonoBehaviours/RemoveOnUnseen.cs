using System.Collections.Generic;
using UnityEngine;

namespace UltraEvents.MonoBehaviours
{
    // Token: 0x02000010 RID: 16
    internal class RemoveOnUnseen : MonoBehaviour
    {
        // Token: 0x06000081 RID: 129 RVA: 0x0000612C File Offset: 0x0000432C
        private void OnBecameInvisible()
        {
            bool flag = false;
            bool flag2 = this.unseens != null || this.unseens.Count == 0;
            if (flag2)
            {
                this.TheisSeen = false;
                foreach (RemoveOnUnseen removeOnUnseen in this.unseens)
                {
                    flag = removeOnUnseen.TheisSeen;
                    bool flag3 = flag;
                    if (flag3)
                    {
                        break;
                    }
                }
            }
            bool flag4 = flag;
            if (!flag4)
            {
                bool flag5 = this.theObject == null;
                if (flag5)
                {
                    Debug.LogWarning("ruh roh raggy");
                }
                bool flag6 = this.theObject.GetComponent<EnemyIdentifier>() != null;
                if (flag6)
                {
                    this.theObject.GetComponent<EnemyIdentifier>().InstaKill();
                }
                Object.Destroy(this.theObject);
            }
        }

        // Token: 0x06000082 RID: 130 RVA: 0x00006220 File Offset: 0x00004420
        private void OnBecameVisible()
        {
            this.TheisSeen = true;
        }

        // Token: 0x04000067 RID: 103
        public GameObject theObject;

        // Token: 0x04000068 RID: 104
        public List<RemoveOnUnseen> unseens = new List<RemoveOnUnseen>();

        // Token: 0x04000069 RID: 105
        public bool TheisSeen = false;
    }
}
