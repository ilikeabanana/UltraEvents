using System;
using UnityEngine;

namespace UltraEvents.MonoBehaviours.Effects
{
    // Token: 0x02000017 RID: 23
    public class AutomaticWeaponsEffectcs : Effect
    {
        // Token: 0x060000A7 RID: 167 RVA: 0x00006CE8 File Offset: 0x00004EE8
        private void Start()
        {
            UltraEventsPlugin.AutomaticFireEffectActive = true;
            RocketLauncher[] array = Resources.FindObjectsOfTypeAll<RocketLauncher>();
            foreach (RocketLauncher rocketLauncher in array)
            {
                rocketLauncher.rateOfFire = 0.01f;
            }
        }

        // Token: 0x060000A8 RID: 168 RVA: 0x00006D24 File Offset: 0x00004F24
        public override void RemoveEffect()
        {
            UltraEventsPlugin.AutomaticFireEffectActive = false;
            RocketLauncher[] array = Resources.FindObjectsOfTypeAll<RocketLauncher>();
            foreach (RocketLauncher rocketLauncher in array)
            {
                rocketLauncher.rateOfFire = 0.25f;
            }
        }
    }
}
