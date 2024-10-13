using System;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    // Token: 0x02000021 RID: 33
    public class Fastmotion : Effect
    {
        // Token: 0x060000C2 RID: 194 RVA: 0x000074E1 File Offset: 0x000056E1
        private void Awake()
        {
            Time.timeScale = 2f;
            MonoSingleton<TimeController>.Instance.timeScaleModifier = 2f;
        }

        // Token: 0x060000C3 RID: 195 RVA: 0x000074FE File Offset: 0x000056FE
        public override void RemoveEffect()
        {
            Time.timeScale = 1f;
            MonoSingleton<TimeController>.Instance.timeScaleModifier = 1f;
            base.RemoveEffect();
        }
    }
}
