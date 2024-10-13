using System;
using System.Collections;
using UltraEvents.Utils;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    // Token: 0x0200001C RID: 28
    public class Lagging : Effect
    {
        // Token: 0x060000B4 RID: 180 RVA: 0x000070B2 File Offset: 0x000052B2
        private void Awake()
        {
            base.StartCoroutine("lag");
        }

        // Token: 0x060000B5 RID: 181 RVA: 0x000070C1 File Offset: 0x000052C1
        private IEnumerator lag()
        {
            while (this.isgud)
            {
                Vector3 pos = ModUtils.GetPlayerTransform().transform.position;
                yield return new WaitForSeconds(this.seconds);
                ModUtils.GetPlayerTransform().transform.position = pos;
                yield return new WaitForSeconds(this.seconds);
                pos = default(Vector3);
            }
            yield break;
        }

        // Token: 0x0400007A RID: 122
        private bool isgud = true;

        // Token: 0x0400007B RID: 123
        private float seconds = 1f;
    }
}
