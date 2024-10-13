using System;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    // Token: 0x02000022 RID: 34
    public class Slowmotion : Effect
    {
        // Token: 0x060000C5 RID: 197 RVA: 0x0000752B File Offset: 0x0000572B
        private void Awake()
        {
            Time.timeScale = 0.3f;
            MonoSingleton<TimeController>.Instance.timeScaleModifier = 0.3f;
        }

        // Token: 0x060000C6 RID: 198 RVA: 0x00007548 File Offset: 0x00005748
        public override void RemoveEffect()
        {
            Time.timeScale = 1f;
            MonoSingleton<TimeController>.Instance.timeScaleModifier = 1f;
            base.RemoveEffect();
        }
    }
}
